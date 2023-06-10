using Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Validation.Validators;
//using System.Linq.Dynamic.Core;
using Database.Scada.Models;
using Base.StaticMethod;

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

        private void SS(PropertyInfo property, object entry, object update)
        {
            var propertyType = property.PropertyType.GetGenericArguments().First();

            var set = Context.Set<Unit>()
                .AsQueryable();
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
            {
                single.SetValue(model, null);
            }

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
            return false;
        }
    }
}
