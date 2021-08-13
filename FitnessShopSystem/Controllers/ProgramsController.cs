namespace FitnessShopSystem.Controllers
{
    using FitnessShopSystem.Data;
    using Microsoft.AspNetCore.Mvc;
    using FitnessShopSystem.Services.Instructors;
    using FitnessShopSystem.Services.Programs;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Models.Programs;

    public class ProgramsController : Controller
    {
        private readonly FitnessShopDbContext data;
        private readonly IInstructorService instructors;
        private readonly IProgramService programs;

        public ProgramsController(
            FitnessShopDbContext data,
            IInstructorService instructors,
            IProgramService programs
           )
        {
            this.data = data;
            this.instructors = instructors;
            this.programs = programs;
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
                    Name = p.Name,
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
    }
}
