using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Role
    {
        public Role()
        {
            Employees = new List<Employee>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}
