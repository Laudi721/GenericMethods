using Dtos.Dtos;
using Office.Controllers.Generic;
using Office.Interfaces.Generic;
using Office.Interfaces;

namespace Office.Controllers
{
    public class RoleController : GenericController<RoleDto>
    {
        private readonly IRoleService _service;

        public RoleController(IGenericService<RoleDto> service, IRoleService roleService) : base(service)
        {
            _service = roleService;
        }
    }
}
