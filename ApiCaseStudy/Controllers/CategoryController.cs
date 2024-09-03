using caselibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCaseStudy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly RetailapplicationContext _context;

        public CategoryController(RetailapplicationContext context)
        {
            _context = context;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET api/<CategoryController>/5
        [HttpGet("api/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory([FromBody] string categoryName)
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

        // POST api/<CategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
