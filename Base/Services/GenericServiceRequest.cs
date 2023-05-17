using Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Services
{
    public abstract partial class GenericService<Model, ModelDto> : IGenericService<ModelDto> where ModelDto : class
                                                                                      where Model : class
    {
        /// <summary>
        /// Metoda do do nadpisania dla niestandarowych operacji
        /// </summary>
        /// <param name="models"></param>
        /// <param name="dto"></param>
        protected virtual void CustomGetMapping(IQueryable<Model> models, List<ModelDto> dtos)
        {
        }

        /// <summary>
        /// Metoda pomocnicza aktualizująca model
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual void PutRequest(ModelDto update)
        {
            IQueryable<Model> query = Context.Set<Model>();

            var model = QueryFilteredByKey(update, ref query).FirstOrDefault();

            var item = StaticMethod.Mapper.Map<Model>(update);

            if(item != null)
                Context.Entry(model).CurrentValues.SetValues(item);

            ModelOperations(model, item);

        }

        /// <summary>
        /// Metoda pomocnicza usuwająca model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual void DeleteRequest(Model model)
        {
            var deletetedItem = DeleteModel(model);

            Context.Entry(model).CurrentValues.SetValues(deletetedItem);
        }

        /// <summary>
        /// Metoda pomocnicza dodająca model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual Model PostRequest(ModelDto item)
        {
            var model = StaticMethod.Mapper.Map<Model>(item);

            ModelOperations(model);

            return model;
        }
    }
}
