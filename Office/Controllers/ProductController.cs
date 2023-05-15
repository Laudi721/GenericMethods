using Base.Controllers;
using Base.Interfaces;
using Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;
using Office.Interfaces;

namespace Office.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : GenericController<ProductDto>
    {
        private readonly IProductService _service;

        public ProductController(IGenericService<ProductDto> service, IProductService productService) : base(service)
        {
            _service = productService;
        }
    }
}
