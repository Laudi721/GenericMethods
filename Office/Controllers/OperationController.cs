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
    public class OperationController : GenericController<OperationDto>
    {
        private readonly IOperationService _service;

        public OperationController(IGenericService<OperationDto> service, IOperationService operationService) : base(service)
        {
            _service = operationService;
        }
    }
}
