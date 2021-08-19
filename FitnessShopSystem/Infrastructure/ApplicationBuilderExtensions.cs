namespace FitnessShopSystem.Infrastructure
{
    using System.Linq;

    using FitnessShopSystem.Data;
    using FitnessShopSystem.Data.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder builder)
        {
            using var scopedServices = builder.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<FitnessShopDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return builder;
        }
        
        private static void SeedCategories(FitnessShopDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "Protein" },
                new Category {Name = "BCAA" },
                new Category {Name = "Creatine" },
                new Category {Name = "Vitamins" },
                new Category {Name = "Gainers" },
                new Category {Name = "Energy" },
            });

            data.SaveChanges();
        }
    }
}
