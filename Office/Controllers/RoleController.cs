using Database.Models;
using Dtos.Dtos;
using Office.Controllers.Generic;
using Office.Interfaces.Generic;
using Office.Interfaces;

namespace Office.Controllers
{
    public class RoleController : GenericController<Role, RoleDto>
    {
        private readonly IRoleService _service;

        public RoleController(IGenericService<Role, RoleDto> service, IRoleService roleService) : base(service)
        {
            _service = roleService;
        }
    }
}
