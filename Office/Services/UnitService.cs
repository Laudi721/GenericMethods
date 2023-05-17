using Base.Services;
using Database.Scada;
using Database.Scada.Models;
using Dtos.Dtos;
using Microsoft.EntityFrameworkCore;
using Office.Interfaces;

namespace Office.Services
{
    public class UnitService : GenericService<Unit, UnitDto>, IUnitService
    {
        public UnitService(ScadaDbContext context) : base(context)
        {
        }

        protected override bool AdditionalCheckBeforeDelete(Unit model)
        {
            if(model.Products.Any())
                return false;

            return base.AdditionalCheckBeforeDelete(model);
        }

        protected override IQueryable<Unit> PreparedQuery()
        {
            return Context.Set<Unit>()
                .Include(a => a.Products)
                .AsQueryable();
        }
    }
}
