using Base.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
//using System.Linq.Dynamic.Core;

namespace Base.Services
{
    public abstract partial class GenericService<Model, ModelDto> : IGenericService<ModelDto> where ModelDto : class
                                                                                              where Model : class
    {
        /// <summary>
        /// Metoda w ktorej wykonywane są operacje na relacjach dodawanego obiektu.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="update"></param>
        protected void ModelOperations(object model, object update = null)
        {
            var singleRelations = model.GetType().GetProperties().Where(a => !a.PropertyType.IsSealed && !typeof(IEnumerable).IsAssignableFrom(a.PropertyType)).ToList();
            var multiRelations = model.GetType().GetProperties().Where(a => !a.PropertyType.IsSealed && typeof(IEnumerable).IsAssignableFrom(a.PropertyType)).ToList();

            foreach (var single in singleRelations)
            {
                var propertyValue = update == null ? single.GetValue(model) : single.GetValue(update);
                var propertyType = single.PropertyType;
                var propertyName = single.Name;

                var keys = propertyType.GetProperties().Where(a => a.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).ToList();
                var key = propertyType.GetProperty("Id");

                var keyValue = key.GetValue(propertyValue);
                var relation = model.GetType().GetProperty($"{propertyName}Id");
                relation.SetValue(model, keyValue);

                single.SetValue(model, null);
            }

            foreach (var multi in multiRelations)
                MapMultiProperty(multi, model, update);

        }

        /// <summary>
        /// Metoda dla operacji wykonywanych dla dodawanego obiektu z kolekcją.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="entry"></param>
        protected void MapMultiProperty(PropertyInfo property, object entry, object update)
        {
            var propertyType = property.PropertyType.GetGenericArguments().First();
            var entryValue = property.GetValue(entry) as IList;
            var modelValue = update != null ? (property.GetValue(update)as IList) : entryValue;

            var collection = Activator.CreateInstance(typeof(List<>).MakeGenericType(propertyType)) as IList;

            if(modelValue != null)
            {
                var set = Context.Set<Model>() //<- tutaj chce dynamiczny model zaleznie co bedzie w propertyType
                    .AsQueryable();

                var set1 = Context.GetType().GetMethods().Where(a => a.Name == "Set" && a.GetGenericArguments().Length == 1 && a.GetParameters().Length == 0);
                var genericSetMethod = set1.Single(m => m.Name == "Set" && m.GetGenericArguments()[0] == propertyType);

                var keys = propertyType.GetProperties().Where(a => a.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).ToList();

                foreach (var item in modelValue)
                {
                    var units = ModelOperationQuery(item, keys, set);

                    foreach (var unit in units)
                    {
                        collection.Add(unit);
                    }
                }
            }

            property.SetValue(entry, collection);
        }

        /// <summary>
        /// Metoda usuwająca obiekty relacyjne i ustawiająca status i czas usunięcia.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Model DeleteModel(Model model)
        {
            var singleRelations = model.GetType()
                .GetProperties()
                .Where(a => !a.PropertyType.IsSealed && !typeof(IEnumerable).IsAssignableFrom(a.PropertyType))
                .ToList();

            var multiRelations = model.GetType()
                .GetProperties()
                .Where(a => !a.PropertyType.IsSealed && typeof(IEnumerable).IsAssignableFrom(a.PropertyType))
                .ToList();

            foreach (var single in singleRelations)            
                single.SetValue(model, null);
            

            foreach (var multi in multiRelations)
                multi.SetValue(model, null);

            var isDeleted = model.GetType().GetProperty("IsDeleted");
            var timeDeleted = model.GetType().GetProperty("TimeDeleted");

            isDeleted.SetValue(model, true);
            timeDeleted.SetValue(model, DateTime.UtcNow);

            return model;
        }

        /// <summary>
        /// Metoda do nadpisywania dla dodatkowego sprawdzenia przed usunieciem obiektu.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual bool AdditionalCheckBeforeDelete(Model model)
        {
            return true;
        }
    }
}
