namespace FitnessShopSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using FitnessShopSystem.Data;
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Models.Deliveries;
    using FitnessShopSystem.Services.Products;
    using AutoMapper;
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
        public IActionResult Order(int id, DeliveryFormModel delivery)
        {
            var product = this.products.Details(id); 

            var userId = this.User.GetId();

            this.deliveries.Create(delivery.CustomerFirstName,
                delivery.CustomerLastName,
                delivery.Company,
                delivery.Address,
                delivery.PostalCode,
                delivery.City,
                delivery.Email,
                delivery.Country,
                delivery.Phone,
                userId,
                product.Id);

            return RedirectToAction("All", "Products");
        }
    }
}
