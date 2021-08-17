﻿namespace FitnessShopSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using FitnessShopSystem.Data.Models;

    public class FitnessShopDbContext : IdentityDbContext
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
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Manufacturer>(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

             builder
                .Entity<TrainingProgram>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Programs)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

             builder
                .Entity<TrainingProgram>()
                .HasOne(p => p.Instructor)
                .WithMany(m => m.Programs)
                .HasForeignKey(p => p.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Instructor>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Instructor>(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Delivery>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Delivery>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
