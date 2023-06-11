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
    public class ContractorService : GenericService<Contractor, ContractorDto>, IContractorService
    {
        public ContractorService(ApplicationContext context) : base(context)
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
