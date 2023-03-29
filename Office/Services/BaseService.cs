using Database;
using Database.Scada;

namespace Office.Services
{
    public class BaseService
    {
        public ScadaDbContext Context;

        public BaseService(ScadaDbContext context) {  Context = context; }
    }
}
