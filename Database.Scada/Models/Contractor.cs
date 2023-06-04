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
    [Table("Contractors")]
    public class Contractor : IModel
    {
        public Contractor()
        {
            ProductionOrders = new List<ProductionOrder>();
            Transactions = new List<Transaction>();
            Contacts = new List<Contact>();
        }

        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? TimeDeleted { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Address Address { get; set; }

        public int AddressId { get; set; }

        public virtual List<Contact> Contacts{ get; set; }

        public string NIP { get; set; }

        public string REGON { get; set; }

        public virtual List<ProductionOrder> ProductionOrders { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
    }
}
