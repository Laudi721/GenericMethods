using Base.Services;
using Database.Scada;
using Database.Scada.Models;
using Dtos.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Office.Interfaces;

namespace Office.Services
{
    public class EmployeeService : GenericService<Employee, EmployeeDto>, IEmployeeService
    {
        //private readonly IPasswordHasher<EmployeeDto> _passwordHasher;
        public EmployeeService(ScadaDbContext context) : base(context)
        {
            //_passwordHasher = passwordHasher;
        }

        public bool Delete(int id)
        {
            var user = Context.Set<Employee>()
                .FirstOrDefault(a => a.Id == id);

            if (user != null)
            {
                user.IsDeleted = true;
                Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override async Task<bool> PostAsync([FromBody] EmployeeDto item)
        {
            var models = PreparedQuery();

            var exist = models.FirstOrDefault(a => a.Login == item.Login);

            if (exist != null)
                return false;

            var model = PostRequest(item);

            Context.Set<Employee>().Add(model);

            model.Role = null;
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        public bool Put([FromBody] EmployeeDto employeeDto)
        {

            throw new NotImplementedException();
        }

        //public override Employee PostRequest(EmployeeDto item)
        //{
        //    var model = new Employee
        //    {
        //        Login = item.Login,
        //        Name = item.Name,
        //        Surname = item.Surname,
        //        Hired = item.Hired,
        //        Role = MapDtoToModel(item.Role),
        //    };

        //    model.Password = Base.StaticMethod.HashHelper.HashPassword(model, item.Password);

        //    return model;
        //}

        protected override List<Employee> PreparedQuery()
        {
            return Context.Set<Employee>()
                .Include(a => a.Role)
                .ToList();
        }
    }
}
