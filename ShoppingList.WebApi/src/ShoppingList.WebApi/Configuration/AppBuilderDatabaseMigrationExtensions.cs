using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.WebApi.Repositories;

namespace ShoppingList.WebApi.Configuration
{
    public static class AppBuilderDatabaseMigrationExtensions
    {
        public static async Task InitializeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                await serviceScope.ServiceProvider.GetRequiredService<ShoppingListDbContext>().Database.MigrateAsync();
            }
        }
    }
}
