using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMicroservices.Models;
using System.Threading.Tasks;
using static CartService;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;
    private readonly RetailapplicationContext _context;
    public CartController(CartService cartService, RetailapplicationContext context)
    {
        _cartService = cartService;
        _context = context;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
    {
        if (dto == null || dto.Quantity <= 0)
        {
            return BadRequest("Invalid data.");
        }

        var result = await _cartService.AddToCartAsync(dto);

        if (result)
        {
            return Ok("Item added to cart.");
        }
        else
        {
            return StatusCode(500, "An error occurred while adding the item to the cart.");
        }
    }


        [HttpGet("quantity/{userId}/{productId}")]
        public async Task<ActionResult<CartItemQuantityDto>> GetCartItemQuantity(int userId, int productId)
        {
            var quantity = await _context.CartItems
            .Join(
                    _context.Carts,
                    cartItem => cartItem.CartId,
                    cart => cart.CartId,
                    (cartItem, cart) => new { cartItem, cart }
                )
                .Where(joined => joined.cart.UserId == userId && joined.cartItem.ProductId == productId)
                .Select(joined => joined.cartItem.Quantity)
                .FirstOrDefaultAsync();

            if (quantity == 0)
            {
                return NotFound(); // Return 404 if no quantity found
            }

            var result = new CartItemQuantityDto { Quantity = quantity };
            return Ok(result);
        }

    public class CartItemQuantityDto
    {
        public int Quantity { get; set; }

        public string ProductName { get; set; }
       
        public decimal Price { get; set; }
        public byte[] Productimages { get; set; }

        public int ProductId    { get; set; }
    }

    [HttpGet("items/{userId}")]
    public async Task<ActionResult<IEnumerable<CartItemQuantityDto>>> GetCartItems(int userId)
    {
        var cartItems = await _context.CartItems
            .Where(ci => ci.Cart.UserId == userId)
            .Select(ci => new CartItemQuantityDto
            {
                ProductId=ci.Product.ProductId,
                ProductName = ci.Product.ProductName,
                Quantity = ci.Quantity,
                Price = ci.Product.Price,
                Productimages = ci.Product.Productimages
            })
            .ToListAsync();

        return Ok(cartItems);
    }

}

