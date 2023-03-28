using AutoMapper;
using Database;
using Database.Models;
using Dtos.Dtos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Office.Interfaces;
using Office.Services.Generic;
using System.Data.Entity;
using System.Runtime.InteropServices;

namespace Office.Services
{
    public class EmployeeService : GenericService<Employee, EmployeeDto>, IEmployeeService
    {
        private readonly IPasswordHasher<EmployeeDto> _passwordHasher;
        public EmployeeService(Scada context, IPasswordHasher<EmployeeDto> passwordHasher) : base(context)
        {
            _passwordHasher = passwordHasher;
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

        public IQueryable<EmployeeDto> Get()
        {
            var employees = Context.Set<Employee>()
                .Include(a => a.Role)
                .Where(a => a.Id != 1 && !a.IsDeleted)
                .ToList();

            var result = employees.Select(a => new EmployeeDto
            {
                Id = a.Id, 
                Name = a.Name,
                Surname = a.Surname,
                Login = a.Login,
                Password = a.Password,
                Role = new RoleDto
                {
                    Id = a.Role.Id, 
                    Name = a.Role.Name
                },
                Fired = a.Fired,
                Hired = a.Hired
            }).AsQueryable();

            return result;
        }

        public bool Post([FromBody] EmployeeDto employeeDto)
        {
            var employee = Context.Set<Employee>()
                .FirstOrDefault(a => a.Login == employeeDto.Login);

            if(employee != null)
                return false;

            var employeeModel = CreateModelObject(employeeDto);

            try
            {
                Context.Add(employeeModel);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception($"Nieudana próba zapisu nowego pracownika o loginie {nameof(employeeDto.Login)} do bazy danych.");
            }
        }

        public bool Put([FromBody] EmployeeDto employeeDto)
        {

            throw new NotImplementedException();
        }

        public Employee CreateModelObject(EmployeeDto employeeDto)
        {
            var model = new Employee();

            var password = _passwordHasher.HashPassword(employeeDto, employeeDto.Password);

            model.Login = employeeDto.Login;
            model.Password = password;
            model.Name = employeeDto.Name;
            model.Surname = employeeDto.Surname;
            model.RoleId = employeeDto.Role.Id;
            model.Hired = employeeDto.Hired;
            model.Fired = employeeDto.Fired;

            return model;
        }

        protected override void CustomGetMapping(List<Employee> models, List<EmployeeDto> dtos)
        {
            var result = models.Select(model => new EmployeeDto
            {
                Id = model.Id,
                Name = model.Name,
                Login = model.Login,
                Surname = model.Surname,
                Password = model.Password,
            }).ToList();

            dtos.AddRange(result);
        }
    }
}
