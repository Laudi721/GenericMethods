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
    public class ContractorService : GenericService<Contractor, ContractorDto>, IContractorService
    {
        public ContractorService(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<Contractor> PreparedQuery()
        {
            return Context.Set<Contractor>()
                .Include(a => a.Addresses)
                .Include(a => a.ProductionOrders)
                .AsQueryable();
        }
    }
}
