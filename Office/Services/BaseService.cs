using Database;

namespace Office.Services
{
    public class BaseService
    {
        public SCADA Context;

        public BaseService(SCADA context) {  Context = context; }
    }
}
