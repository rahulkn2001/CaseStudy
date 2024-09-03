using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMicroservices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductMicroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private readonly RetailapplicationContext _context;
        public ProductController(RetailapplicationContext context)
        {
            _context = context;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("bycategory/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory([FromRoute] string categoryName)
        {
            var products = await _context.Products
                .Where(p => p.Category.CategoryName == categoryName)
                .Select(p => new ProductDto
                {
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryName
                })
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

        public class ProductDto
        {
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public string CategoryName { get; set; }
        }

       
    }
}
