using Base.Interfaces;

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
