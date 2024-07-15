using Microsoft.AspNetCore.Mvc;

namespace BFFCopilotApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IProfileManagement _profileManagement;

        public ShoppingCartController(IProfileManagement profileManagement)
        {
            _profileManagement = profileManagement;
        }

        [HttpPost("CreateProfile")]
        public IActionResult CreateProfile([FromBody] User user)
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
        public IActionResult UpdateProfile(int userId, [FromBody] User updatedUser)
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
        public IActionResult GetProfile(int userId)
        {
            var user = _profileManagement.GetUserById(userId);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Profile not found.");
            }
        }
    }
}
