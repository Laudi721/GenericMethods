using Database.Scada.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Scada.Models
{
    [Table("Roles")]
    public class Role : BaseModel
    {
        public Role()
        {
            Employees = new List<Employee>();
        }
        public string Name { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}
