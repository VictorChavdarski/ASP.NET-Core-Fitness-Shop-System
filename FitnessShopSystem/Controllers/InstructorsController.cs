namespace FitnessShopSystem.Controllers
{
    using System.Threading.Tasks;

    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Models.Instructors;
    using FitnessShopSystem.Services.Instructors;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    public class InstructorsController : Controller
    {
        private readonly IInstructorService instructors;

        public InstructorsController(IInstructorService instructors)
            => this.instructors = instructors;

        [Authorize]
        public IActionResult Create() => View();
     

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BecomeInstructorFormModel instructor)
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

            await this.instructors.CreateAsync(
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
