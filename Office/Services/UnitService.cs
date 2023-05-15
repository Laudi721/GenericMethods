using Base.Services;
using Database.Scada;
using Database.Scada.Models;
using Dtos.Dtos;
using Office.Interfaces;

namespace Office.Services
{
    public class UnitService : GenericService<Unit, UnitDto>, IUnitService
    {
        public UnitService(ScadaDbContext context) : base(context)
        {
        }
    }
}
