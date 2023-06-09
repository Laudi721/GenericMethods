using Database;
using Database.Scada;

namespace Office.Services
{
    public class BaseService
    {
        public ApplicationDbContext Context;

        public BaseService(ApplicationDbContext context) {  Context = context; }
    }
}
