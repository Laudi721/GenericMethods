using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Dtos
{
    public class RoleDto
    {
        public RoleDto()
        {
            Employees = new List<EmployeeDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<EmployeeDto> Employees { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
