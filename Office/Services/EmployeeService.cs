using Database;
using Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;
using Office.Interfaces;

namespace Office.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        public EmployeeService(SCADA context) : base(context)
        {
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EmployeeDto> Get()
        {
            throw new NotImplementedException();
        }

        public bool Post([FromBody] EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        public bool Put([FromBody] EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
