using Base.Controllers;
using Base.Interfaces;
using Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;
using Office.Interfaces;

namespace Office.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitController : GenericController<UnitDto>
    {
        private readonly IUnitService _service;

        public UnitController(IGenericService<UnitDto> service, IUnitService unitService) : base(service)
        {
            _service = unitService;
        }
    }
}
