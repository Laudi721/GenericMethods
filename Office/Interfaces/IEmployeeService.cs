using Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Office.Interfaces
{
    public interface IEmployeeService
    {
        public IQueryable<EmployeeDto> Get();

        public bool Post([FromBody] EmployeeDto employeeDto);

        public bool Delete(int id);

        public bool Put([FromBody] EmployeeDto employeeDto);
    }
}
