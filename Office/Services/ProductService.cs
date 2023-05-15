using Base.Services;
using Database.Scada;
using Database.Scada.Models;
using Dtos.Dtos;
using Office.Interfaces;
using System.Data.Entity;

namespace Office.Services
{
    public class ProductService : GenericService<Product, ProductDto>, IProductService
    {
        public ProductService(ScadaDbContext context) : base(context)
        {
        }

        protected override IList<Product> PreparedQuery()
        {
            return Context.Set<Product>()
                .Include(a => a.Unit)
                .ToList();
        }
    }
}
