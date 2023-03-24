using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
