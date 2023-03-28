using AutoMapper;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Office.Interfaces.Generic;
using System.Reflection;

namespace Office.Services.Generic
{
    public abstract class GenericService<TModel, TDto> : IGenericService<TModel, TDto>  where TModel : class 
    {
        protected readonly Scada Context;

        public GenericService(Scada context)
        {
            Context = context;
        }

        public virtual async Task<List<TDto>> GetAsync()
        {
            var models = await Context.Set<TModel>().ToListAsync();

            var result = new List<TDto>();

            CustomGetMapping(models, result);

            return result;
        }

        public virtual bool PostAsync([FromBody] TDto dto)
        {
            var model = Context.Set<TModel>()
                .Find(dto);

            if (model != null)
                return false;

            var newModel = typeof(TModel);

            return true;
        }

        protected virtual void CustomGetMapping(List<TModel> models, List<TDto> dto)
        {
            foreach (var model in models)
            {
                var obj = typeof(TDto);


            }
        }
    }
}
