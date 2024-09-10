using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace BFFCopilotApi.Services
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                // Look for any board games.
                if (context.Users.Any())
                {
                    return;   // Data was already seeded
                }
                else
                {
                    context.Users.AddRange(
                      new User
                      {
                          UserId = 1,
                          Name = "admin",
                          Mobile = "admin"
                      },
                     new User
                     {
                         UserId = 2,
                         Name = "admin2",
                         Mobile = "admin2"
                     });

                    context.SaveChanges();
                }
                
            }
        }
    }
}
