using Microsoft.EntityFrameworkCore;

namespace BFFCopilotApi.Services
{
    public class ProfileManagement : IProfileManagement
    {
        private readonly DataContext _context;
        private readonly IServiceProvider _serviceProvider;
        public ProfileManagement(IServiceProvider serviceProvider,DataContext context)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<int> CreateProfile(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.UserId; // Assuming UserId is the primary key and is set by the database upon insertion
        }

        public async Task<bool> UpdateUserId(int userId, User updatedUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return false;
            }

            // Update properties
            user.Name = updatedUser.Name;
            // Update other properties as needed

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            return user;
        }

        public void CreateProfileData()
        {
            DataGenerator.Initialize(_serviceProvider);
        }

        public async Task<List<User>> ViewAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}

