using Database.Scada.Base;
using Database.Scada.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.GenericMethods.Models
{
    [Table("Addresses")]
    public class Address : IModel
    {
        public Address()
        {
            Contractors = new List<Contractor>();
        }

        [Key]
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public string PostalCode { get; set; }

        public virtual List<Contractor> Contractors { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? TimeDeleted { get; set; }
    }
}
