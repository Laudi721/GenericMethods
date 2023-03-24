using Database;

namespace Office.Services
{
    public class BaseService
    {
        public SCADADbContext Context;

        public BaseService(SCADADbContext context) {  Context = context; }
    }
}
