namespace FitnessShopSystem.Controllers
{
    using System.Linq;

    using FitnessShopSystem.Data;
    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Models.Instructors;
    using FitnessShopSystem.Data.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class InstructorsController : Controller
    {
        private readonly FitnessShopDbContext data;

        public InstructorsController(FitnessShopDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeInstructorFormModel instructor)
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
                return View(instructor);
            }

            var instructorData = new Instructor
            {
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Age = instructor.Age,
                PhoneNumber = instructor.PhoneNumber,
                Email = instructor.PhoneNumber,
                UserId = userId
            };

            this.data.Instructors.Add(instructorData);
            this.data.SaveChanges();

            return RedirectToAction("Mine", "Programs");
        }
    }
}
