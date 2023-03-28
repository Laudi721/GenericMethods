using Microsoft.AspNetCore.Mvc;

namespace Office.Interfaces.Generic
{
    public interface IGenericService<TModel, TDto> 
    {
        //abstract Task<IQueryable<TDto>> GetAsync(TModel model, TDto dto);
        Task<List<TDto>> GetAsync();

        bool PostAsync([FromBody] TDto dto);
    }
}
