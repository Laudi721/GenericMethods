using Base.Services;
using Database.Scada;
using Database.Scada.Models;
using Dtos.Dtos;
using Microsoft.EntityFrameworkCore;
using Office.Interfaces;

namespace Office.Services
{
    public class ProductService : GenericService<Product, ProductDto>, IProductService
    {
        public ProductService(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<Product> PreparedQuery()
        {
            return Context.Set<Product>()
                .Include(a => a.Unit)
                .AsQueryable();
        }
    }
}
