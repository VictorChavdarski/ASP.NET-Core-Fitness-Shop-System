namespace FitnessShopSystem.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Models.Deliveries;
    using FitnessShopSystem.Services.Products;
    using FitnessShopSystem.Services.Deliveries;

    public class DeliveriesController : Controller
    {
        private readonly IProductService products;
        private readonly IDeliveryService deliveries;

        public DeliveriesController(
            IProductService products,
            IDeliveryService deliveries)
        {
            this.products = products;
            this.deliveries = deliveries;
        }

        [Authorize]
        public IActionResult Order() => View(); 

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Order(int id, DeliveryFormModel delivery)
        {
            var product = this.products.Details(id); 

            var userId = this.User.GetId();

            if (!ModelState.IsValid)
            {
                return View(delivery);
            }

            await this.deliveries.CreateAsync(delivery.CustomerFirstName,
                delivery.CustomerLastName,
                delivery.Company,
                delivery.Address,
                delivery.PostalCode,
                delivery.City,
                delivery.Email,
                delivery.Country,
                delivery.Phone,
                product.Id);

            return RedirectToAction("All", "Products");
        }

        public IActionResult Information() => View();
    }
}
