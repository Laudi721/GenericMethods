using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Office.Interfaces.Generic;

namespace Office.Controllers.Generic
{
    [ApiController,Route("[controller]")]
    public abstract class GenericController<TModel, TDto> : ControllerBase
    {
        private readonly IGenericService<TModel, TDto> _service;

        public GenericController(IGenericService<TModel, TDto> service)
        {
            _service = service;
        }

        [HttpGet, Route("GetAsync")]
        public virtual async Task<ActionResult<IQueryable<TDto>>> GetAsync()
        {
            var result = await _service.GetAsync();

            return Ok(result);
        }

        [HttpPost, Route("PostAsync")]
        public virtual async Task<ActionResult<TDto>> PostAsync([FromBody] TDto dto)
        {
            var result = _service.PostAsync(dto);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

    }
}
