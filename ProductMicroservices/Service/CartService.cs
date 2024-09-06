using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductMicroservices.Models;

public class CartService
{
    private readonly RetailapplicationContext _context;

    public CartService(RetailapplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> AddToCartAsync(AddToCartDto dto)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == dto.UserId);

        if (cart == null)
        {
            // Create a new cart if one doesn't exist for the user
            cart = new Cart { UserId = dto.UserId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        var existingCartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == dto.ProductId);

        if (existingCartItem != null)
        {
            // Update quantity if item already exists in the cart
            existingCartItem.Quantity = dto.Quantity;
        }
        else
        {
            // Add new item to the cart
            var newCartItem = new CartItem
            {
                CartId = cart.CartId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };
            _context.CartItems.Add(newCartItem);
        }

        return await _context.SaveChangesAsync() > 0;
    }
    public class AddToCartDto
    {
        public int UserId { get; set; } // Assuming you have the user ID available
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
