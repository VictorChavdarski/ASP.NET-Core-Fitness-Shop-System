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

    public class DeliveriesController : Controller
    {
        private readonly IProductService products;
        private readonly FitnessShopDbContext data;
        private readonly IMapper mapper;

        public DeliveriesController(IProductService products,
            FitnessShopDbContext data, IMapper mapper)
        {
            this.products = products;
            this.data = data;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Order() => View(); 


        [Authorize]
        [HttpPost]
        public IActionResult Order(int id, DeliveryFormModel delivery)
        {
            var product = this.products.Details(id); 

            var userId = this.User.GetId();

            var deliveryData = new Delivery
            {
                CustomerFirstName = delivery.CustomerFirstName,
                CustomerLastName = delivery.CustomerFirstName,
                Company = delivery.Company,
                Address = delivery.Address,
                PostalCode = delivery.PostalCode,
                City = delivery.City,
                Email = delivery.Email,
                Country = delivery.Country,
                Phone = delivery.Phone,
                UserId = userId,
                ProductId = product.Id,
            };

            this.data.Deliveries.Add(deliveryData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Products");
        }
    }
}
