using Base.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Controllers
{
    [ApiController, Route("[controller]")]
    public abstract class GenericController<ModelDto> : ControllerBase
    {
        private readonly IGenericService<ModelDto> _service;

        public GenericController(IGenericService<ModelDto> service)
        {
            _service = service;
        }

        [HttpGet, Route("GetAsync")]
        public virtual async Task<IQueryable<ModelDto>> GetAsync()
        {
            var result = await _service.GetAsync();

            return await _service.GetAsync();
        }

        [HttpPost, Route("PostAsync")]
        public virtual async Task<ActionResult> PostAsync([FromBody] ModelDto dto)
        {
            var result = await _service.PostAsync(dto);

            if (result)
                return Ok();
            else
                return BadRequest();
        }
    }
}
