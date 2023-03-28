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
    public class EmployeeController : GenericController<Employee, EmployeeDto>
    {
        private readonly IEmployeeService _employeeService;
        //public EmployeeController(IEmployeeService employeeService)
        //{
        //    _employeeService = employeeService;
        //}

        public EmployeeController(IGenericService<Employee, EmployeeDto> service, IEmployeeService employeeService)
    : base(service)
        {
            _employeeService = employeeService;
        }

        public override async Task<ActionResult<IQueryable<EmployeeDto>>> GetAsync()
        {
            var result = await _employeeService.GetAsync();

            return Ok(result);
        }

        public override async Task<ActionResult<EmployeeDto>> PostAsync([FromBody] EmployeeDto dto)
        {
            if(_employeeService.Post(dto))
                return Ok();
            else
                return BadRequest();
        }
    }
}
