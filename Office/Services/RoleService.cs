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

        protected override void CustomGetMapping(List<Role> models, List<RoleDto> dto)
        {
            var result = models.Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            dto.AddRange(result);
        }

        public override Role PostRequest(RoleDto item)
        {
            // do napisania
            var model = Activator.CreateInstance(typeof(Role)) as Role;

            model.Name = item.Name;

            return model;
        }
    }
}
