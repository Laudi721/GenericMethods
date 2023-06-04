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
    [Table("Supplies")]
    public class Supply
    {
        public bool IsDeleted { get; set; }

        public DateTime? TimeDeleted { get; set; }

        public Product Product { get; set; }

        [Key]
        public int ProductId { get; set; }

        public decimal Quantity { get; set; }
    }
}
