using Base.Services;
using Database.Scada;
using Database.Scada.Models;
using Dtos.Dtos;
using Microsoft.EntityFrameworkCore;
using Office.Interfaces;

namespace Office.Services
{
    public class RoleService : GenericService<Role, RoleDto>, IRoleService
    {
        public RoleService(ApplicationDbContext context) : base(context)
        {
        }

        protected override void CustomGetMapping(IQueryable<Role> models, List<RoleDto> dtos)
        {
            foreach(var model in models.Where(a => a.IsDeleted))
            {
                var item = dtos.FirstOrDefault(a => a.Id == model.Id);
                dtos.Remove(item);
            }
        }

        protected override bool AdditionalCheckBeforeDelete(Role model)
        {
            // Wartość "1" zawsze bedzie administratorem seedowanym wraz z pierwszym uruchomieniem aplikacji.
            if (model.Employees.Any() || model.Id == 1) 
                return false;

            return base.AdditionalCheckBeforeDelete(model);
        }

        protected override IQueryable<Role> PreparedQuery()
        {
            return Context.Set<Role>()
                .Include(a => a.Employees)
                .AsQueryable();
        }
    }
}
