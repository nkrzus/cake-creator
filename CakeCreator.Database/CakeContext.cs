using CakeCreator.Model;
using Microsoft.EntityFrameworkCore;

namespace CakeCreator.Database
{
    public class CakeContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<CakeIngredient> CakeIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CakeCreatorDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

    }
}
