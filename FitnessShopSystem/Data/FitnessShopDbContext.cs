namespace FitnessShopSystem.Data
{
    using FitnessShopSystem.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FitnessShopDbContext : IdentityDbContext
    {
        public FitnessShopDbContext(DbContextOptions<FitnessShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }

        public DbSet<Category> Categories { get; init; }
    }
}
