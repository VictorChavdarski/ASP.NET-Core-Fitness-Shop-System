namespace FitnessShopSystem.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Models.Programs;
    using FitnessShopSystem.Services.Programs;
    using FitnessShopSystem.Services.Instructors;
    using FitnessShopSystem.Services.Programs.Models;

    using AutoMapper;

    public class ProgramsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProgramService programs;
        private readonly IInstructorService instructors;

        public ProgramsController(
            IMapper mapper,
            IProgramService programs,
            IInstructorService instructors)
        {
            this.mapper = mapper;
            this.programs = programs;
            this.instructors = instructors;
        }

        public IActionResult All([FromQuery] ProgramSearchQueryModel query)
        {
            var queryResult = this.programs.All(query.Name,query.SearchTerm,query.Sorting,query.CurrentPage);

            var programLevels = this.programs.AllProgramLevels();

            query.TotalPrograms = queryResult.TotalPrograms;
            query.Levels = programLevels;
            query.Programs = queryResult.Programs.Select(p => new ProgramEditViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Level = p.Level,
                ImageUrl = p.ImageUrl
            });

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.instructors.IsInstructor(this.User.GetId()))
            {
                return RedirectToAction(nameof(InstructorsController.Create), "Instructors");
            }

            return View(new ProgramFormModel
            {
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ProgramFormModel program)
        {
            var instructorId = this.instructors.GetId(this.User.GetId());

            if (instructorId == 0)
            {
                return RedirectToAction(nameof(InstructorsController.Create), "Instructors");
            }

            if (!ModelState.IsValid)
            {
                return View(program);
            }

            await this.programs.CreateAsync(
               program.Name,
               program.Level,
               program.Description,
               program.ImageUrl,
               instructorId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var program = this.programs.Details(id);

            var programData = this.mapper.Map<ProgramDetailsServiceModel>(program);

            return View(programData);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await this.programs.DeleteAsync(id);

            return RedirectToAction(nameof(Mine));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.instructors.IsInstructor(userId))
            {
                return RedirectToAction(nameof(InstructorsController.Create), "Instructors");
            }

            var program = this.programs.Details(id);

            if (program.UserId != userId)
            {
                return Unauthorized();
            }

            var programForm = this.mapper.Map<ProgramFormModel>(program);

            return View(programForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, ProgramServiceModel program)
        {
            if (!this.instructors.IsInstructor(this.User.GetId()))
            {
                return RedirectToAction(nameof(InstructorsController.Create), "Instructors");
            }

            var instructorId = this.instructors.GetId(this.User.GetId());

            if (instructorId == 0)
            {
                return RedirectToAction(nameof(InstructorsController.Create), "Instructors");
            }

            if (!ModelState.IsValid)
            {
                return View(program);
            }

            var editedProgram = this.programs.Edit(
               id,
               program.Name,
               program.Description,
               program.Level,
               program.ImageUrl,
               instructorId);

            if (!editedProgram)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Mine));
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myPrograms = this.programs.ByUser(this.User.GetId());

            return View(myPrograms);
        }
    }
}
