using BFFCopilotApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFFCopilotApi
{
    public static class Configure
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IProfileManagement, ProfileManagement>();
            
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "ShoppingCart"));
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

    }
}
