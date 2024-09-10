using Microsoft.AspNetCore.Identity;

namespace BFFCopilotApi.Services
{
    public class ApplicationUser : IdentityUser
    {
        // Add additional properties here
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
