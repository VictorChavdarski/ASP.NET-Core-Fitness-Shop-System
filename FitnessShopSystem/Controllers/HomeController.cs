namespace FitnessShopSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using FitnessShopSystem.Data;
    using FitnessShopSystem.Models.Home;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class HomeController : Controller
    {
        private readonly FitnessShopDbContext data;
        private readonly IMapper mapper;

        public HomeController(FitnessShopDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }


        public IActionResult Index()
        {
            var totalProducts = this.data.Products.Count();

            var products = this.data
                .Products
                .OrderByDescending(p => p.Id)
                .ProjectTo<ProductIndexViewModel>(this.mapper.ConfigurationProvider)
                .Take(4)
                .ToList();

            return View(new IndexViewModel
            {
                TotalProducts = totalProducts,
                Products = products
            });
        }

        public IActionResult Error()  => View();
    }
}
