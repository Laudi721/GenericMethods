using Database;

namespace Office.Services
{
    public class BaseService
    {
        public ApplicationContext Context;

        public BaseService(ApplicationContext context) {  Context = context; }
    }
}
