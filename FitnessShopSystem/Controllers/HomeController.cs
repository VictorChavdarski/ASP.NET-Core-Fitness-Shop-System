namespace FitnessShopSystem.Controllers
{
    using System.Linq;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using FitnessShopSystem.Data;
    using FitnessShopSystem.Models;
    using FitnessShopSystem.Models.Products;
    using FitnessShopSystem.Models.Home;

    public class HomeController : Controller
    {
        private readonly FitnessShopDbContext data;

        public HomeController(FitnessShopDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var totalProducts = this.data.Products.Count();

            var products = this.data
                .Products
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductIndexViewModel
                {
                    Brand = p.Brand,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description
                })
                .Take(4)
                .ToList();

            return View(new IndexViewModel
            {
                TotalProducts = totalProducts,
                Products = products
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
