using Database.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    [Table("Operations")]
    public class Operation : IModel
    {
        public Operation()
        {
            ProductionOrders = new List<ProductionOrder>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<ProductionOrder> ProductionOrders { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? TimeDeleted { get; set; }
    }
}
