using Microsoft.AspNetCore.Mvc;

namespace Office.Interfaces.Generic
{
    public interface IGenericService<ModelDto> 
    {
        Task<IQueryable<ModelDto>> GetAsync();

        Task<bool> PostAsync([FromBody] ModelDto dto);
    }
}
