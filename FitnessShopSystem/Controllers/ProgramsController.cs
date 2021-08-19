namespace FitnessShopSystem.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using FitnessShopSystem.Data;
    using FitnessShopSystem.Services.Instructors;
    using FitnessShopSystem.Services.Programs;
    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Models.Programs;
    using FitnessShopSystem.Services.Programs.Models;

    using AutoMapper;
    using System.Threading.Tasks;

    public class ProgramsController : Controller
    {
        private readonly FitnessShopDbContext data;
        private readonly IInstructorService instructors;
        private readonly IProgramService programs;
        private readonly IMapper mapper;

        public ProgramsController(
            FitnessShopDbContext data,
            IInstructorService instructors,
            IProgramService programs,
            IMapper mapper)
        {
            this.data = data;
            this.instructors = instructors;
            this.programs = programs;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var program = this.programs.Details(id);

            var programData = this.mapper.Map<ProgramDetailsServiceModel>(program);

            return View(programData);

        }

        public IActionResult All([FromQuery] ProgramSearchQueryModel query)
        {
            var programQuery = this.data.Programs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                programQuery = programQuery.Where(p => p.Name == query.Name);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                programQuery = programQuery.Where(p =>
                       p.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                       p.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            programQuery = query.Sorting switch
            {
                ProgramSorting.Level => programQuery.OrderBy(p => p.Level),
                ProgramSorting.DateCreated or _ => programQuery.OrderByDescending(p => p.Id)
            };

            var totalPrograms = programQuery.Count();

            var programs = programQuery
                .Skip((query.CurrentPage - 1) * ProgramSearchQueryModel.ProgramsPerPage)
                .Take(ProgramSearchQueryModel.ProgramsPerPage)
                .Select(p => new ProgramEditViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    Level = p.Level
                })
                .ToList();

            var programLevels = this.data
                .Programs
                .Select(p => p.Level)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

            query.TotalPrograms = totalPrograms;
            query.Levels = programLevels;
            query.Programs = programs;

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
                Categories = this.programs.AllProgramCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ProgramFormModel program)
        {
            var instructorId = this.instructors.GetId(this.User.GetId());

            if (instructorId == 0)
            {
                return RedirectToAction(nameof(InstructorsController.Create), "Instructors");
            }

            if (!this.programs.CategoryExist(program.CategoryId))
            {
                this.ModelState.AddModelError(nameof(program.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                program.Categories = this.programs.AllProgramCategories();

                return View(program);
            }

            this.programs.Create(
               program.Name,
               program.Level,
               program.Description,
               program.ImageUrl,
               program.CategoryId,
               instructorId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myPrograms = this.programs.ByUser(this.User.GetId());

            return View(myPrograms);
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

            programForm.Categories = this.programs.AllProgramCategories();

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
               program.CategoryId,
               instructorId);

            if (!editedProgram)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await this.programs.DeleteAsync(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
