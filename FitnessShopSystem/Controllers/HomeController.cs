namespace FitnessShopSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using FitnessShopSystem.Models.Home;
    using FitnessShopSystem.Services.Products;
    using FitnessShopSystem.Services.Programs;

    public class HomeController : Controller
    {
        private readonly IProductService products;
        private readonly IProgramService programs;

        public HomeController(
            IProductService products,
            IProgramService programs)
        {
            this.products = products;
            this.programs = programs;
        }

        public IActionResult Index()
        {
            var totalProducts = this.products.TotalProducts();

            var latestProducts = this.products.Latest();

            var latestPrograms = this.programs.Latest();

            return View(new IndexViewModel
            {
                TotalProducts = totalProducts,
                Products = latestProducts,
                Programs = latestPrograms
            });
        }

        public IActionResult Error() => View();

        public IActionResult Join() => View();

        [Authorize]
        public IActionResult Chat() => View();
    }
}
