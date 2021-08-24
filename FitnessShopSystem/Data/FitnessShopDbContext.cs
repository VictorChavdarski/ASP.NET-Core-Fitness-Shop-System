namespace FitnessShopSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using FitnessShopSystem.Data.Models;

    public class FitnessShopDbContext : IdentityDbContext<User>
    {
        public FitnessShopDbContext(DbContextOptions<FitnessShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<TrainingProgram> Programs { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

             builder
                .Entity<Manufacturer>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Manufacturer>(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

             builder
                .Entity<TrainingProgram>()
                .HasOne(p => p.Instructor)
                .WithMany(m => m.Programs)
                .HasForeignKey(p => p.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Instructor>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Instructor>(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Contact>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Contact>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
