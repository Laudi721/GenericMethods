using Base.Controllers;
using Base.Interfaces;
using Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;
using Office.Interfaces;

namespace Office.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : GenericController<EmployeeDto>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IGenericService<EmployeeDto> service, IEmployeeService employeeService)
            : base(service)
        {
            _employeeService = employeeService;
        }
    }
}
