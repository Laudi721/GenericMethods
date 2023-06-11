using Base.Controllers;
using Base.Interfaces;
using Dtos.Dtos;
using Office.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Controllers
{
    public class ProductionOrderController : GenericController<ProductionOrderDto>
    {
        private readonly IProductionOrderService _service;

        public ProductionOrderController(IGenericService<ProductionOrderDto> service, IProductionOrderService prodOrderService) : base(service)
        {
            _service = prodOrderService;
        }
    }
}
