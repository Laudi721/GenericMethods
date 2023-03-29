using AutoMapper;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Office.Interfaces.Generic;
using System.Reflection;

namespace Office.Services.Generic
{
    public abstract class GenericService<Model, ModelDto> : IGenericService<ModelDto>  where Model : class 
    {
        protected readonly Scada Context;
        private readonly Dictionary<Type, List<PropertyInfo>> typePropertyCache;

        public GenericService(Scada context)
        {
            Context = context;
            typePropertyCache = new Dictionary<Type, List<PropertyInfo>>();
        }

        public virtual async Task<IQueryable<ModelDto>> GetAsync()
        {
            var query = PreparedQuery();

            var result = new List<ModelDto>();

            CustomGetMapping(query, result);

            return result.AsQueryable();
        }

        public virtual async Task<bool> PostAsync([FromBody] ModelDto item)
        {
            //var model = Context.Set<Model>();

            var model = PostRequest(item);

            Context.Set<Model>().Add(model);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        protected virtual void CustomGetMapping(List<Model> models, List<ModelDto> dto)
        {
            // do napisania
        }

        public virtual Model PostRequest(ModelDto item)
        {
            // do napisania
            var model = Activator.CreateInstance(typeof(Model)) as Model;

            return model;
        }

        protected virtual List<Model> PreparedQuery()
        {
            return Context.Set<Model>().ToList();
        }


    }
}
