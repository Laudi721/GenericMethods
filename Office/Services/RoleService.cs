using Base.Services;
using Database.Scada;
using Database.Scada.Models;
using Dtos.Dtos;
using Office.Interfaces;

namespace Office.Services
{
    public class RoleService : GenericService<Role, RoleDto>, IRoleService
    {
        public RoleService(ScadaDbContext context) : base(context)
        {
        }

        //public override Role PostRequest(RoleDto item)
        //{
        //    // do napisania
        //    var model = Activator.CreateInstance(typeof(Role)) as Role;

        //    model.Name = item.Name;

        //    return model;
        //}
    }
}
