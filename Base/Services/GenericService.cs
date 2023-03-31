using AutoMapper;
using Base.Interfaces;
using Database.Scada;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Base.Services
{
    public abstract class GenericService<Model, ModelDto> : IGenericService<ModelDto> where Model : class
    {
        protected readonly ScadaDbContext Context;
        private readonly IMapper _mapper;

        public GenericService(ScadaDbContext context)
        {
            Context = context;
        }
        public GenericService(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Generyczna metoda zwracająca dane z modelu
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IQueryable<ModelDto>> GetAsync()
        {
            var query = PreparedQuery();

            var result = new List<ModelDto>();

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

            var deletedItem = DeleteRequest(item, query);

            Context.Set<Model>().Update(deletedItem);
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
        /// Generyczna metoda aktualizująca model
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual async Task<bool> PutAsync([FromBody] ModelDto update)
        {
            var query = PreparedQuery();

            var updateItem = PutRequest(update, query);

            Context.Set<Model>().Update(updateItem);

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
        /// Metoda do mapowania modelu na dto
        /// </summary>
        /// <param name="models"></param>
        /// <param name="dto"></param>
        protected virtual void CustomGetMapping(List<Model> models, List<ModelDto> dto)
        {
            // do napisania
        }

        /// <summary>
        /// Metoda pomocnicza aktualizująca model
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual Model PutRequest(ModelDto update, List<Model> query)
        {
            // do napisania
            var model = Activator.CreateInstance(typeof(Model)) as Model;

            return model;
        }

        /// <summary>
        /// Metoda pomocnicza usuwająca model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual Model DeleteRequest(ModelDto item, List<Model> query)
        {
            //do napisania
            var model = Activator.CreateInstance(typeof(Model)) as Model;


            return model;
        }


        /// <summary>
        /// Metoda pomocnicza dodająca model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual Model PostRequest(ModelDto item)
        {
            // do napisania
            var model = Activator.CreateInstance(typeof(Model)) as Model;

            var modelProperties = model.GetType().GetProperties().ToList();
            var itemProperties = item.GetType().GetProperties().ToList();


            var ss = Base.StaticMethod.Mapper.Map<ModelDto, Model>(item);


            return model;
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
        /// Metoda mapujaca dto na model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual Model MapDtoToModel(ModelDto item)
        {
            // do napisania
            var model = Activator.CreateInstance(typeof(Model)) as Model;

            return model;
        }
    }
}
