using Base.Services;
using Database.Scada;
using Database.Scada.Models;
using Dtos.Dtos;
using Office.Interfaces;
using System.Data.Entity;
using System.Web.Http;

namespace Office.Services
{
    public class RoleService : GenericService<Role, RoleDto>, IRoleService
    {
        public RoleService(ScadaDbContext context) : base(context)
        {
        }

        //public override async Task<bool> DeleteAsync([FromBody] RoleDto item)
        //{
        //    var query = PreparedQuery();

        //    var model = DeleteRequest(item, query);

        //    Context.Set<Role>().Update(model);
        //    try
        //    {
        //        await Context.SaveChangesAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        // obsluga erroru do napisania
        //        return false;
        //    }

        //    return true;
        //}

        protected override void CustomGetMapping(List<Role> models, List<RoleDto> dtos)
        {
            foreach(var model in models.Where(a => a.IsDeleted))
            {
                var item = dtos.FirstOrDefault(a => a.Id == model.Id);
                dtos.Remove(item);
            }
        }

        //public override Role DeleteRequest(RoleDto item, List<Role> query)
        //{
        //    var model = query.FirstOrDefault(a => a.Id == item.Id);

        //    model.IsDeleted = true;
        //    model.TimeDeleted = DateTime.UtcNow;
        //    model.Employees = null;

        //    return model;
        //}

        protected override List<Role> PreparedQuery()
        {
            return Context.Set<Role>()
                .Include(a => a.Employees)
                .ToList();
        }
    }
}
