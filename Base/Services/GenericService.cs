using Base.Interfaces;
using Database.Scada;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.Services
{
    public abstract class GenericService<Model, ModelDto> : IGenericService<ModelDto> where Model : class
    {
        protected readonly ScadaDbContext Context;
        private readonly Dictionary<Type, List<PropertyInfo>> typePropertyCache;

        public GenericService(ScadaDbContext context)
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
            catch (Exception e)
            {
                throw new Exception(e.ToString());
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

        protected virtual Model MapDtoToModel(ModelDto item)
        {
            // do napisania
            var model = Activator.CreateInstance(typeof(Model)) as Model;

            return model;
        }
    }
}
