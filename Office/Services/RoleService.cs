using Database;
using Database.Models;
using Dtos.Dtos;
using Office.Interfaces;
using Office.Services.Generic;

namespace Office.Services
{
    public class RoleService : GenericService<Role, RoleDto>, IRoleService
    {
        public RoleService(Scada context) : base(context)
        {
        }
    }
}
