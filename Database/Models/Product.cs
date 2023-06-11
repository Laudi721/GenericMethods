using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Base;

namespace Database.Models
{
    [Table("Products")]
    public class Product : IModel
    {
        public Product()
        {
            ProductionOrders = new List<ProductionOrder>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public Unit Unit { get; set; }

        public int UnitId { get; set; }

        public virtual List<ProductionOrder> ProductionOrders { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TimeDeleted { get; set; }
    }
}
