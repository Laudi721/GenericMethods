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
    public class Role
    {
        public Role()
        {
            Employees = new List<Employee>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}
