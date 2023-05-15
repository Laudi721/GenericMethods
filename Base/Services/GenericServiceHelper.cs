using Base.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.Services
{
    public abstract partial class GenericService<Model, ModelDto> : IGenericService<ModelDto> where ModelDto : class
                                                                                              where Model : class
    {
        protected void ModelPostOperations(object model)
        {
            var singleRelations = model.GetType().GetProperties().Where(a => !a.PropertyType.IsSealed && !typeof(IEnumerable).IsAssignableFrom(a.PropertyType)).ToList();
            var multiRelations = model.GetType().GetProperties().Where(a => !a.PropertyType.IsSealed && typeof(IEnumerable).IsAssignableFrom(a.PropertyType)).ToList();

            foreach (var single in singleRelations)
            {
                var propertyValue = single.GetValue(model);
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
                MapMultiProperty(multi, model);

        }

        protected void MapMultiProperty(PropertyInfo property, object model)
        {
            var propertyType = property.PropertyType.GetGenericArguments().First();
            var modelValue = property.GetValue(model) as IList;
        }

        private void DeleteModel(Model model)
        {
            var singleRelations = model.GetType().GetProperties().Where(a => !a.PropertyType.IsSealed && !typeof(IEnumerable).IsAssignableFrom(a.PropertyType)).ToList();
            var multiRelations = model.GetType().GetProperties().Where(a => !a.PropertyType.IsSealed && typeof(IEnumerable).IsAssignableFrom(a.PropertyType)).ToList();

            foreach (var single in singleRelations)
            {
                DropRelationKey(single, model);
                single.SetValue(model, null);
            }

            foreach (var multi in multiRelations)
                multi.SetValue(model, null);

            var isDeleted = model.GetType().GetProperty("IsDeleted");
            var timeDeleted = model.GetType().GetProperty("TimeDeleted");

            isDeleted.SetValue(model, true);
            timeDeleted.SetValue(model, DateTime.UtcNow);
        }

        /// <summary>
        /// Metoda do wykopania
        /// </summary>
        /// <param name="model"></param>
        /// <param name="update"></param>
        protected void ModelPostOperations(object model, object update = null)
        {
            var singleRelations = model.GetType().GetProperties().Where(a => !a.PropertyType.IsSealed && !typeof(IEnumerable).IsAssignableFrom(a.PropertyType)).ToList();
            var manyRelations = model.GetType().GetProperties().Where(a => !a.PropertyType.IsSealed && typeof(IEnumerable).IsAssignableFrom(a.PropertyType)).ToList();

            foreach (var single in singleRelations)
            {
                var propertyValue = single.GetValue(model);
                var propertyType = single.PropertyType;
                var propertyName = single.Name;

                var ss = propertyType.GetProperties().ToList();

                if (propertyValue == null)
                    continue;

                //var set = Context.Set(propertyType);
                var keys = propertyType.GetProperties().Where(a => a.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).ToList();


                //var inner = single.PropertyType.GetProperty("Id");
                //var id = inner.GetValue(single);

                //var dd = model.GetType().GetProperty($"{single}Id");

                //dd.SetValue(model, id);

                //single.SetValue(model, null);           
            }

            foreach (var many in (manyRelations as IEnumerable))
            {

            }
        }

        private void DropRelationKey(PropertyInfo property, object model)
        {
            var propertyTye = property.PropertyType;
            var propertyName = property.Name;
            var propertyValue = property.GetValue(model);



        }

        private void SetModelAsDeleted(Model model)
        {
            var isDeleted = model.GetType().GetProperty("IsDeleted");
            var timeDeleted = model.GetType().GetProperty("TimeDeleted");

            if (isDeleted == null || timeDeleted == null)
                return;

            isDeleted.SetValue(model, true);
            timeDeleted.SetValue(model, DateTime.UtcNow);
        }
    }
}
