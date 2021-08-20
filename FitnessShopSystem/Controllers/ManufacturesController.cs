namespace FitnessShopSystem.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using FitnessShopSystem.Models.Manufactures;
    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Services.Manufacturers;

    public class ManufacturesController : Controller
    {
        private readonly IManufacturerService manufacturers;

        public ManufacturesController(IManufacturerService manufacturers)
            => this.manufacturers = manufacturers;

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BecomeManufacturerFormModel manufacturer)
        {
            var userId = this.User.GetId();

            var userIsAlreadyManufacturer = this.manufacturers.IsManufacturer(userId);

            if (userIsAlreadyManufacturer)
            {
                return BadRequest();
            }
            
            if (!ModelState.IsValid)
            {
                return View(manufacturer);
            }

            await this.manufacturers.CreateAsync(
                manufacturer.Name,
                manufacturer.PhoneNumber,
                manufacturer.Company,
                userId);

            return RedirectToAction("Mine", "Products");
        }
    }
}
