using BFFCopilotApi.Models;
using BFFCopilotApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BFFCopilotApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
  
    public class ShoppingCartController : ControllerBase
    {
        private readonly IProfileManagement _profileManagement;
        private readonly ICartService _cartService; 
       

        public ShoppingCartController(IProfileManagement profileManagement,ICartService cartService)
        {
          
            _profileManagement = profileManagement;
            _cartService = cartService;
           
        }

        [HttpPost("CreateProfileData")]
        public async Task<IActionResult> CreateProfileData()
        {
            _profileManagement.CreateProfileData();

            return await ViewAllProfile();
        }

        [HttpPost("CreateProfile")]
        public async Task<IActionResult> CreateProfile([FromBody] User user)
        {
            var userId = _profileManagement.CreateProfile(user);
            if (userId.Result > 0)
            {
                return Ok(new { UserId = userId });
            }
            else
            {
                return BadRequest("Failed to create profile.");
            }
        }

        [HttpPut("UpdateProfile/{userId}")]
        public async Task<IActionResult> UpdateProfile(int userId, [FromBody] User updatedUser)
        {
            var success = _profileManagement.UpdateUserId(userId, updatedUser);
            if (success.Result)
            {
                return Ok("Profile updated successfully.");
            }
            else
            {
                return BadRequest("Failed to update profile.");
            }
        }


        [HttpGet("GetProfile/{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var user = await _profileManagement.GetUserById(userId);
            if (user == null)
            {
                return NotFound("Profile not found."); // Ensure this is returned when user is null
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpGet("ViewAllProfile")]
        public async Task<IActionResult> ViewAllProfile()
        {
            var user = await _profileManagement.ViewAllUsers();
            if (user == null)
            {
                return NotFound("Profile not found."); // Ensure this is returned when user is null
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost("AddCartItem")]
        public async Task<IActionResult> AddCartItem([FromBody] Product product, [FromQuery] int userId)
        {
            await _cartService.AddCartItem(product, new User { UserId = userId });
            return Ok("Item added to cart successfully.");
        }

        [HttpPost("UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem([FromBody] Product product, [FromQuery] int userId)
        {
            await _cartService.UpdateCartItem(product, new User { UserId = userId });
            return Ok("Cart item updated successfully.");
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout([FromBody] ICollection<CartItem> cartItems, [FromQuery] int userId)
        {
            var success = await _cartService.Checkout(cartItems, userId);
            if (success)
            {
                return Ok("Checkout successful.");
            }
            else
            {
                return BadRequest("Checkout failed.");
            }
        }

        // GET: api/ShoppingCart/ViewCartItems/{userId}
        [HttpGet("ViewCartItems/{userId}")]
        public async Task<IActionResult> ViewCartItems(int userId)
        {
            var cartItems = await _cartService.ViewCartItem(userId);
            if (cartItems == null)
            {
                return NotFound("No cart items found for the specified user.");
            }

            return Ok(cartItems);
        }
    }
}
