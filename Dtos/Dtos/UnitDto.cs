using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Dtos
{
    public class UnitDto
    {
        public UnitDto()
        {
            Products = new List<ProductDto>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
