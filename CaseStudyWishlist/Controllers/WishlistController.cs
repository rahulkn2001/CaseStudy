using CaseStudyWishlist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CaseStudyWishlist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly FinalContext _context;

        public WishlistController(FinalContext context)
        {
            _context = context;
        }
        // GET: api/<WishlistController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WishlistController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WishlistController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WishlistController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WishlistController>/5
        [HttpDelete("{productId}")]
        public void Delete(int userId, int productId)
        {
            var res=_context.WishlistItems.FirstOrDefault(x => x.ProductId == productId );

            _context.WishlistItems.Remove(res);
            _context.SaveChanges();

        }



        // GET: api/WishlistItems/user/100
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<WishlistProductDto>>> GetWishlistProductsByUserId(int userId)
        {
            var products = await (from wi in _context.WishlistItems
                                  join p in _context.Products on wi.ProductId equals p.ProductId
                                  join w in _context.Wishlists on wi.WishlistId equals w.WishlistId
                                  where w.UserId == userId
                                  select new WishlistProductDto
                                  {
                                      ProductName = p.ProductName,
                                      Price = p.Price
                                  }).ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }
        [HttpPost("{userId}/{productId}")]
        public async Task AddToWishlistAsync(int userId, int productId)
        {
            var wishlist = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = userId
                };
                _context.Wishlists.Add(wishlist);
                await _context.SaveChangesAsync();
            }
            var wishlistItem = new WishlistItem
            {
                WishlistId = wishlist.WishlistId,
                ProductId = productId
            };
            _context.WishlistItems.Add(wishlistItem);
            await _context.SaveChangesAsync();
        }
    }

    public class WishlistProductDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }

}


