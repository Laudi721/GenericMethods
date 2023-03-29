using Database.Models;
using Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Office.Interfaces.Generic;

namespace Office.Interfaces
{
    public interface IEmployeeService : IGenericService<EmployeeDto>
    {
        //public IQueryable<EmployeeDto> Get();

        //public bool Post([FromBody] EmployeeDto employeeDto);

        //public bool Delete(int id);

        //public bool Put([FromBody] EmployeeDto employeeDto);
    }
}
