using Base.Services;
using Database.Scada;
using Database.Scada.Models;
using Dtos.Dtos;
using Microsoft.EntityFrameworkCore;
using Office.Interfaces;
using System.Web.Http;

namespace Office.Services
{
    public class EmployeeService : GenericService<Employee, EmployeeDto>, IEmployeeService
    {
        public EmployeeService(ScadaDbContext context) : base(context)
        {
        }

        public override async Task<bool> PostAsync([FromBody] EmployeeDto item)
        {
            var models = PreparedQuery();

            var exist = models.FirstOrDefault(a => a.Login == item.Login);

            if (exist != null)
                return false;

            var model = PostRequest(item);

            var hashedPassword = Base.StaticMethod.HashHelper.HashPassword(model, model.Password);
            model.Password = hashedPassword;

            Context.Set<Employee>().Add(model);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }

            return true;
        }

        protected override bool AdditionalCheckBeforeDelete(Employee model)
        {
            //Wartość "1" zawsze bedzie administratorem seedowanym wraz z pierwszym uruchomieniem aplikacji.
            if (model.Id == 1) 
                return false;

            return base.AdditionalCheckBeforeDelete(model);
        }

        protected override IQueryable<Employee> PreparedQuery()
        {
            return Context.Set<Employee>()
                .Include(a => a.Role)
                .AsQueryable();
        }
    }
}
