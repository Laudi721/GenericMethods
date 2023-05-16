using Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Base.Services
{
    public abstract partial class GenericService<Model, ModelDto> : IGenericService<ModelDto> where ModelDto : class
                                                                                  where Model : class
    {
        /// <summary>
        /// Metoda przygotowująca dane z bazy pod wybrany model
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<Model> PreparedQuery()
        {
            return Context.Set<Model>().AsQueryable();
        }

        protected virtual IQueryable<Model> QueryFilteredByKey(ModelDto item, ref IQueryable<Model> query)
        {
            var keys = item.GetType().GetProperties().Where(a => a.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).ToList();

            var queryContidion = QueryCondition(item, keys);

            query = query.Where(queryContidion);

            return query;
        }

        private string QueryCondition(object obj, IEnumerable<PropertyInfo> keys)
        {
            var dynamicQuery = new StringBuilder();

            foreach (var key in keys)
            {
                dynamic value = key.GetValue(obj);

                if (value == 0)
                    return null;

                if (dynamicQuery.Length == 0)
                    dynamicQuery.AppendFormat($"{key.Name} = {value}");
                else
                    dynamicQuery.AppendFormat($" and {key.Name} = {value}");
            }

            return dynamicQuery.ToString();
        }

        protected virtual IQueryable<Model> DeleteQuery(ModelDto item)
        {
            IQueryable<Model> query = Context.Set<Model>();

            return QueryFilteredByKey(item, ref query);
        }
    }
}
