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
    public class ContractorController : GenericController<ContractorDto>
    {
        private readonly IContractorService _service;

        public ContractorController(IGenericService<ContractorDto> service, IContractorService contractorService) : base(service)
        {
            _service = contractorService;
        }
    }
}
