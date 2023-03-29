using Dtos.Dtos;
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
