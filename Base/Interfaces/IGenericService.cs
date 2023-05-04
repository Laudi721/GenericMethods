using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Base.Interfaces
{
    public interface IGenericService<ModelDto>
    {
        Task<IQueryable<ModelDto>> GetAsync();

        Task<bool> PostAsync([FromBody] ModelDto item);

        Task<bool> DeleteAsync([FromBody] ModelDto item);

        Task<bool> PutAsync([FromBody] ModelDto update);
    }
}
