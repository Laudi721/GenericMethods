using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Dtos
{
    public class AddressDto
    {
        public AddressDto()
        {
            Contractors = new List<ContractorDto>();
        }

        [Key]
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int BuildingNumber { get; set; }

        public string PostalCode { get; set; }

        public List<ContractorDto> Contractors{ get; set; }
    }
}
