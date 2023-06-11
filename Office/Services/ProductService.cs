using Base.Services;
using Database;
using Database.Models;
using Dtos.Dtos;
using Microsoft.EntityFrameworkCore;
using Office.Interfaces;

namespace Office.Services
{
    public class ProductService : GenericService<Product, ProductDto>, IProductService
    {
        public ProductService(ApplicationContext context) : base(context)
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
