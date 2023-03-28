using Database;

namespace Office.Services
{
    public class BaseService
    {
        public Scada Context;

        public BaseService(Scada context) {  Context = context; }
    }
}
