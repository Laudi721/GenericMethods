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
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public bool Put([FromBody] EmployeeDto employeeDto)
        {

            throw new NotImplementedException();
        }

        //public Employee CreateModelObject(EmployeeDto employeeDto)
        //{
        //    var model = new Employee();

        //    var password = _passwordHasher.HashPassword(employeeDto, employeeDto.Password);

        //    model.Login = employeeDto.Login;
        //    model.Password = password;
        //    model.Name = employeeDto.Name;
        //    model.Surname = employeeDto.Surname;
        //    model.RoleId = employeeDto.Role.Id;
        //    model.Hired = employeeDto.Hired;
        //    model.Fired = employeeDto.Fired;

        //    return model;
        //}

        protected override void CustomGetMapping(List<Employee> models, List<EmployeeDto> dtos)
        {
            var result = models.Select(model => new EmployeeDto
            {
                Id = model.Id,
                Name = model.Name,
                Login = model.Login,
                Surname = model.Surname,
                Password = model.Password,
                //Role = new RoleDto
                //{
                //    Id = model.RoleId,
                //    Name = model.Role.Name,
                //}
            }).ToList();

            dtos.AddRange(result);
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

        protected virtual Role MapDtoToModel(RoleDto item)
        {
            var contex = Context.Set<Role>()
                .FirstOrDefault(a => a.Id == item.Id);

            return contex;
        }
    }
}
