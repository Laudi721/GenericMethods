using Base.Services;
using Database;
using Database.Models;
using Dtos.Dtos;
using Microsoft.EntityFrameworkCore;
using Office.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Services
{
    public class OperationService : GenericService<Operation, OperationDto>, IOperationService
    {
        public OperationService(ApplicationContext context) : base(context)
        {
        }

        protected override IQueryable<Operation> PreparedQuery()
        {
            return Context.Set<Operation>()
                .Include(a => a.ProductionOrders)
                .AsQueryable();
        }
    }
}
