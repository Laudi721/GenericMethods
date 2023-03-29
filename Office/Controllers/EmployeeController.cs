using Database.Models;
using Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;
using Office.Controllers.Generic;
using Office.Interfaces;
using Office.Interfaces.Generic;
using System.Diagnostics.Eventing.Reader;

namespace Office.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : GenericController<EmployeeDto>
    {
        private readonly IEmployeeService _employeeService;
        //public EmployeeController(IEmployeeService employeeService)
        //{
        //    _employeeService = employeeService;
        //}

        public EmployeeController(IGenericService<EmployeeDto> service, IEmployeeService employeeService)
    : base(service)
        {
            _employeeService = employeeService;
        }

        public override async Task<IQueryable<EmployeeDto>> GetAsync()
        {
            var result = await _employeeService.GetAsync();

            return result;
        }

        public override async Task<ActionResult> PostAsync([FromBody] EmployeeDto dto)
        {
            if(await _employeeService.PostAsync(dto))
                return Ok();
            else
                return BadRequest();
        }
    }
}
