using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Dtos
{
    public class ContractorDto
    {
        public ContractorDto()
        {
            Addresses = new List<AddressDto>();
            ProductionOrders = new List<ProductionOrderDto>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<AddressDto> Addresses { get; set; }

        public string NIP { get; set; }

        public string REGON{ get; set; }

        public List<ProductionOrderDto> ProductionOrders { get; set; }
    }
}
