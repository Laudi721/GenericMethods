using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Dtos
{
    public class EmployeeDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Hired{ get; set; }

        public DateTime Fired { get; set; }
    }
}
