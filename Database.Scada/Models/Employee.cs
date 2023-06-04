using Database.Scada.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Scada.Models
{
    [Table("Employees")]
    public class Employee : IModel
    {
        public Employee()
        {
            Skills = new List<Skill>();
        }

        [Key]
        public int Id { get; set ; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public virtual Role Role { get; set; }

        public int? RoleId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Hired { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Fired { get; set; }

        public bool IsFired { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? TimeDeleted { get; set; }

        public virtual List<Skill> Skills { get; set; }
    }
}
