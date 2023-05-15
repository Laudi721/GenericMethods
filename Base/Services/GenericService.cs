using Base.Interfaces;
using Database.Scada;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Http;

namespace Base.Services
{
    public abstract partial class GenericService<Model, ModelDto> : IGenericService<ModelDto> where ModelDto : class
                                                                                      where Model : class
    {
        protected readonly ScadaDbContext Context;

        public GenericService(ScadaDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Generyczna metoda zwracająca dane z modelu
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IQueryable<ModelDto>> GetAsync()
        {
            var query = PreparedQuery();

            var models = query.ToList();

            var result = StaticMethod.Mapper.MapCollection<ModelDto>(models);

            CustomGetMapping(query, result);

            return result.AsQueryable();
        }

        /// <summary>
        /// Generyczna metoda tworząca nowy model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> PostAsync([FromBody] ModelDto item)
        {
            var model = PostRequest(item);

            Context.Set<Model>().Add(model);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

            return true;
        }

        /// <summary>
        /// Generyczna metoda usuwająca model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> DeleteAsync([FromBody] ModelDto item)
        {
            var query = PreparedQuery();

            var model = DeleteRequest(item, query);

            Context.Set<Model>().Update(model);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // obsluga erroru do napisania
                return false;
            }

            return true;
        }

        /// <summary>
        /// Metoda aktualizująca model
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual async Task<bool> PutAsync([FromBody] ModelDto update)
        {
            var query = PreparedQuery();

            PutRequest(update, query);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // obsluga erroru do napisania
                return false;
            }

            return true;
        }

        // Generic Methods






        /// <summary>
        /// Metoda pomocnicza dodająca model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual Model PostRequest(ModelDto item)
        { 
            var model = StaticMethod.Mapper.Map<Model>(item);

            ModelPostOperations(model);

            return model;
        }

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

            foreach(var multi in multiRelations)
                multi.SetValue(model, null);

            var isDeleted = model.GetType().GetProperty("IsDeleted");
            var timeDeleted = model.GetType().GetProperty("TimeDeleted");

            isDeleted.SetValue(model, true);
            timeDeleted.SetValue(model, DateTime.UtcNow);
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

        /// <summary>
        /// Metoda przygotowująca dane z bazy pod wybrany model
        /// </summary>
        /// <returns></returns>
        protected virtual List<Model> PreparedQuery()
        {
            return Context.Set<Model>().ToList();
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
    }
}
