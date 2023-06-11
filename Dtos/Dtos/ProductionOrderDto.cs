using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Dtos
{
    public class ProductionOrderDto
    {
        public ProductionOrderDto()
        {
            Operations = new List<OperationDto>();
        }

        [Key]
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public ContractorDto Contractor { get; set; }

        public ProductDto Product { get; set; }

        public decimal Quantity { get; set; }
               
        public DateTime CreatedDateTime { get; set; }

        public DateTime RealizationDateTime { get; set;}

        public List<OperationDto> Operations { get; set; }
    }
}
