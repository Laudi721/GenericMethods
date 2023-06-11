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
    public class AddressController : GenericController<AddressDto>
    {
        private readonly IAddressService _service;

        public AddressController(IGenericService<AddressDto> service, IAddressService addressService) : base(service)
        {
            _service = addressService;
        }
    }
}
