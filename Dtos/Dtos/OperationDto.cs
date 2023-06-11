using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Dtos
{
    public class OperationDto
    {
        public OperationDto()
        {
            ProductionOrders = new List<ProductionOrderDto>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProductionOrderDto> ProductionOrders { get; set; }
    }
}
