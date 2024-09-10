using BFFCopilotApi.Models;

namespace BFFCopilotApi.Services
{
    public interface ICartService
    {
        Task AddCartItem(Product product, User user);
        Task UpdateCartItem(Product product, User user);

        Task<IEnumerable<CartItem>> ViewCartItem(int userId);

        Task<bool> Checkout(ICollection<CartItem> cartItems, int userId);
    }
}
