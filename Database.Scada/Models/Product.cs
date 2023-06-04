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

        public Unit Unit{ get; set; }

        public int UnitId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? TimeDeleted { get; set; }

        public StockState StockState { get; set; }

        public int StockStateId { get; set; }

        public virtual List<ProductionOrder> ProductionOrders { get; set; }
    }
}
