using caselibrary.Models;
using caselibrary.repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ApiCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly RetailapplicationContext _context;
        private readonly IConfiguration _config;
        private readonly IUser _iu;

        public CartController(RetailapplicationContext context, IConfiguration config, IUser iu)
        {
            _context = context;
            _config = config;
            _iu = iu;
        }

        // GET: api/Cart
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Cart/5
        [HttpGet("{userId}")]
        public ActionResult<IList<string>> GetProductNamesForUser(int userId)
        {
           
            var cartIds = _context.Carts
                .Where(c => c.UserId == userId)
                .Select(c => c.CartId)
                .ToList(); 

           
            var productIds = _context.CartItems
                .Where(ci => cartIds.Contains(ci.CartId ?? 0)) 
                .Select(ci => ci.ProductId ?? 0) 
                .Distinct()
                .ToList(); 
            var productNames = _context.Products
                .Where(p => productIds.Contains(p.ProductId)) 
                .Select(p => p.ProductName)
                .ToList(); 

            return Ok(productNames);
        }
    }
}
