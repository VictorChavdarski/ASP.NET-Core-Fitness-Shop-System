namespace FitnessShopSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using FitnessShopSystem.Models.Home;
    using FitnessShopSystem.Services.Products;

    public class HomeController : Controller
    {
        private readonly IProductService products;

        public HomeController(
            IProductService products)
        {
            this.products = products;
        }

        public IActionResult Index()
        {
            var totalProducts = this.products.TotalProducts();

            var latestProducts = this.products.Latest(); 

            return View(new IndexViewModel
            {
                TotalProducts = totalProducts,
                Products = latestProducts
            });
        }

        public IActionResult Error() => View();

        public IActionResult Join() => View();

        [Authorize]
        public IActionResult Chat() => View();
    }
}
