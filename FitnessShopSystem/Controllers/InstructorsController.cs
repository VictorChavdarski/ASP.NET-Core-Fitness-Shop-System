namespace FitnessShopSystem.Controllers
{
    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Models.Instructors;
    using FitnessShopSystem.Services.Instructors;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class InstructorsController : Controller
    {
        private readonly IInstructorService instructors;

        public InstructorsController(IInstructorService instructors)
            => this.instructors = instructors;

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeInstructorFormModel instructor)
        {
            var userId = this.User.GetId();

            var userIsAlreadyInstructor = this.instructors.IsInstructor(userId);

            if (userIsAlreadyInstructor)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(instructor);
            }

            this.instructors.Create(
                instructor.FirstName,
                instructor.LastName,
                instructor.Age,
                instructor.PhoneNumber,
                instructor.Email,
                userId);

            return RedirectToAction("Mine", "Programs");
        }
    }
}
