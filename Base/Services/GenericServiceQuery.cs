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
        /// Metoda przygotowująca dane z bazy pod wybrany model
        /// </summary>
        /// <returns></returns>
        protected virtual IList<Model> PreparedQuery()
        {
            return Context.Set<Model>().ToList();
        }
    }
}
