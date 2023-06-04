using Database.Scada.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Database.Scada.Models
{
    [Table("Transactions")]
    public class Transaction : IModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PaymentCreditingTime { get; set; }

        public Contractor Contractor{ get; set; }

        public int ContractorId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? TimeDeleted { get; set; }
    }
}
