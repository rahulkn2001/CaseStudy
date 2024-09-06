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
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryName,
                    Specification=p.Specification,
                    Images=p.Productimages
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
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public string CategoryName { get; set; }

            public string Specification { get; set; }

            public byte[] Images { get; set; }
        }

        [HttpGet("products/{productId}")]
        public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] int productId)
        {
            var product = await _context.Products
                .Where(p => p.ProductId == productId)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryName,
                    Specification = p.Specification 
                    , Images = p.Productimages
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("averageratings/{productId}")]
        public async Task<ActionResult<ProductRatingDto>> GetAverageRatingByProductId(int productId)
        {
            var rating = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .GroupBy(r => r.ProductId)
                .Select(g => new ProductRatingDto
                {
                    ProductId = (int)g.Key,
                    AvgRating = (double)g.Average(r => r.Rating)
                })
                .FirstOrDefaultAsync(); // Use FirstOrDefaultAsync to get a single result or null

            if (rating == null)
            {
                return Ok(4);
            }

            return Ok(rating);
        }

        public class ProductRatingDto
        {
            public int ProductId { get; set; }
            public double AvgRating { get; set; }
        }

    }
}
