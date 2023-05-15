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
        protected virtual void CustomGetMapping(IList<Model> models, List<ModelDto> dtos)
        {
        }

        /// <summary>
        /// Metoda pomocnicza aktualizująca model
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual void PutRequest(ModelDto update, IList<Model> query)
        {
            var model = StaticMethod.Mapper.Map<Model>(update);

        }

        /// <summary>
        /// Metoda pomocnicza usuwająca model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual Model DeleteRequest(ModelDto item, IList<Model> query)
        {
            var model = StaticMethod.Mapper.Map<Model>(item);

            //SetModelAsDeleted(model);
            DeleteModel(model);

            return model;
        }
    }
}
