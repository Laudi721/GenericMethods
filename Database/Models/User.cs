using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class User 
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedTime { get; set; }

        public virtual Role Role { get; set; }

        public int RoleId { get; set; }

    }
}
