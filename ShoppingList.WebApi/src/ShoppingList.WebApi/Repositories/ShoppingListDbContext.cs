using Microsoft.EntityFrameworkCore;
using ShoppingList.WebApi.Models;

namespace ShoppingList.WebApi.Repositories
{
    public class ShoppingListDbContext : DbContext
    {
        public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Item> Items { get; set; }
    }
}
