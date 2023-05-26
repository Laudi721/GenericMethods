using Base.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Base.Controllers
{
    [Route("[controller]")]
    public abstract class GenericController<ModelDto> : ControllerBase
    {
        private readonly IGenericService<ModelDto> _service;

        public GenericController(IGenericService<ModelDto> service)
        {
            _service = service;
        }

        /// <summary>
        /// Glowny endpoint Get
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Get")]
        public virtual async Task<IQueryable<ModelDto>> Get()
        {
            return await _service.GetAsync();
        }

        /// <summary>
        /// Glowny endpoint Post
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost, Route("Post")]
        public virtual async Task<IActionResult> Post([FromBody] ModelDto item)
        {
            var result = await _service.PostAsync(item);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Glowny endpoint Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost, Route("Delete")]
        public virtual async Task<IActionResult> Delete([FromBody] ModelDto item)
        {
            var result = await _service.DeleteAsync(item);

            if(result)
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Glowny endpoint Put
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        [HttpPost, Route("Put")]
        public virtual async Task<IActionResult> Put([FromBody] ModelDto update)
        {
            var result = await _service.PutAsync(update);

            if(result)
                return Ok();
            else 
                return BadRequest();
        }
    }
}
