using Base.Services;
using Database.GenericMethods.Models;
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
    public class AddressService : GenericService<Address, AddressDto>, IAddressService
    {
        public AddressService(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<Address> PreparedQuery()
        {
            return Context.Set<Address>()
                .Include(a => a.Contractors)
                .AsQueryable();
        }
    }
}
