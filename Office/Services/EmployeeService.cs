﻿using Base.Services;
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

        protected override void CustomGetMapping(IQueryable<Employee> models, List<EmployeeDto> dtos)
        {
            foreach(var model in models.Where(a => a.IsDeleted))
            {
                var item = dtos.FirstOrDefault(a => a.Id == model.Id);
                dtos.Remove(item);
            }
        }

        protected override IQueryable<Employee> PreparedQuery()
        {
            return Context.Set<Employee>()
                .Include(a => a.Role)
                .AsQueryable();
        }
    }
}
