using Base.Controllers;
using Base.Interfaces;
using Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;
using Office.Interfaces;

namespace Office.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : GenericController<RoleDto>
    {
        private readonly IRoleService _serviceRole;

        public RoleController(IGenericService<RoleDto> service, IRoleService roleService) : base(service)
        {
            _serviceRole = roleService;
        }
    }
}
