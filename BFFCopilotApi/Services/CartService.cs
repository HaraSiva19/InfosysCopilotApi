using BFFCopilotApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BFFCopilotApi.Services
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }

        public async Task AddCartItem(Product product, User user)
        {
            var cartItem = new CartItem
            {
                // Assuming CartItem has ProductId, UserId, Quantity, etc.
                ProductId = product.ProductId,
                CustomerId = user.UserId,
                Quantity = 1 // Example default quantity
            };

            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem>> ViewCartItem(int userId)
        {
            // Retrieve cart items for the specified user
            return await _context.CartItems
                .Include(ci => ci.Product) // Include related product details
                .Where(ci => ci.CustomerId == userId)
                .ToListAsync();
        }

        public async Task UpdateCartItem(Product product, User user)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == product.ProductId && ci.CustomerId == user.UserId);

            if (cartItem != null)
            {
                // Update cart item details as needed
                cartItem.Quantity += 1; // Example increment quantity

                _context.CartItems.Update(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Checkout(ICollection<CartItem> cartItems, int userId)
        {
            // Example checkout logic
            foreach (var item in cartItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null || product.Stock < item.Quantity)
                {
                    // Product not found or not enough stock
                    return false;
                }

                // Deduct stock
                product.Stock -= item.Quantity;
                _context.Products.Update(product);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
