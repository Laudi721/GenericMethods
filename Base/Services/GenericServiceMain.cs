using Base.Interfaces;
using Database.Scada;
using Microsoft.EntityFrameworkCore;
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

            var model = await DeleteQuery(item).FirstOrDefaultAsync();

            if (model == null)
                return false;

            model = query.FirstOrDefault(a => a.Equals(model));

            if (!AdditionalCheckBeforeDelete(model))
            {
                Console.WriteLine($"ConsoleLog: Błąd podczas usuwania modelu z bazy: {typeof(Model).Name}");
                return false;
            }

            DeleteRequest(model);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
                throw new Exception(e.ToString());
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
            PutRequest(update);

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
    }
}
