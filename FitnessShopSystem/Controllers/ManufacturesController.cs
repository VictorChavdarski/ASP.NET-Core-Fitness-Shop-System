namespace FitnessShopSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using FitnessShopSystem.Data;
    using FitnessShopSystem.Models.Manufactures;
    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Data.Models;

    public class ManufacturesController : Controller
    {
        private readonly FitnessShopDbContext data;

        public ManufacturesController(FitnessShopDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeManufacturerFormModel manufacturer)
        {
            var userId = this.User.GetId();

            var userIsAlreadyManufacturer = this.data
                .Manufacturers
                .Any(m => m.UserId == userId);

            if (userIsAlreadyManufacturer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(manufacturer);
            }

            var manufacturerData = new Manufacturer
            {
                Name = manufacturer.Name,
                PhoneNumber = manufacturer.PhoneNumber,
                UserId = userId
            };

            this.data.Manufacturers.Add(manufacturerData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Products");
        }
    }
}
