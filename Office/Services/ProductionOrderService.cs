using Base.Services;
using Database.Scada;
using Database.Scada.Models;
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
    public class ProductionOrderService : GenericService<ProductionOrder, ProductionOrderDto>, IProductionOrderService
    {
        public ProductionOrderService(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<ProductionOrder> PreparedQuery()
        {
            return Context.Set<ProductionOrder>()
                .Include(a => a.Product)
                .Include(a => a.Contractor)
                .Include(a => a.Operations)
                .AsQueryable();
        }
    }
}
