using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMicroservices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductMicroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {

        private readonly RetailapplicationContext _context;
        public InventoryController(RetailapplicationContext context)
        {
            _context = context;
        }
        // GET: api/<InventoryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

     
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductStockDto>> GetProductStock(int productId)
        {
            var productStock= await _context.Products
                .Where(p => p.ProductId == productId)
                .Select(p => new ProductStockDto
                {
                    ProductName = p.ProductName,
                    StockQuantity = p.Inventory.StockQuantity
                })
                .FirstOrDefaultAsync();
            return Ok(productStock);

        }

        public class ProductStockDto
        {
            public string ProductName { get; set; }
            public int StockQuantity { get; set; }
        }
    }
}
