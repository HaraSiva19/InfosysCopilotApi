using BFFCopilotApi.Models;

namespace BFFCopilotApi.Services
{
    public interface ICartService
    {
        void AddCartItem(Product product, User user);
        void UpdateCartItem(Product product, User user);
    }
}
