using FitnessShopSystem.Data;
using FitnessShopSystem.Data.Models;
using FitnessShopSystem.Models.Programs;
using System.Collections.Generic;
using System.Linq;

namespace FitnessShopSystem.Services.Programs
{
    public class ProgramService : IProgramService
    {
        private readonly FitnessShopDbContext data;

        public ProgramService(FitnessShopDbContext data)
        {
            this.data = data;
        }

        public ProgramQueryServiceModel All(string name, string searchTerm, ProgramSorting sorting, int currentPage)
        {
            var programQuery = this.data.Programs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                programQuery = programQuery.Where(p => p.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                programQuery = programQuery.Where(p =>
                       p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                       p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            programQuery = sorting switch
            {
                ProgramSorting.Level => programQuery.OrderBy(p => p.Level),
                ProgramSorting.DateCreated or _ => programQuery.OrderByDescending(p => p.Id)
            };

            var totalPrograms = programQuery.Count();

            var programs = GetPrograms(programQuery
                .Skip((currentPage - 1) * ProgramSearchQueryModel.ProgramsPerPage)
                .Take(ProgramSearchQueryModel.ProgramsPerPage));


            return new ProgramQueryServiceModel
            {
                TotalPrograms = totalPrograms,
                CurrentPage = currentPage,
                Programs = programs
            };
        }

        public IEnumerable<ProgramCategoryServiceModel> AllProgramCategories()
        => this.data
                .Categories
                .Select(p => new ProgramCategoryServiceModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

        public IEnumerable<ProgramServiceModel> ByUser(string userId)
                    => GetPrograms(this.data
                .Programs
                .Where(p => p.Instructor.UserId == userId));

        public bool CategoryExist(int categoryId)
         => this.data
                .Categories
                .Any(c => c.Id == categoryId);

        public int Create(string name, string level, string description, int categoryId, int instructorId)
        {
            var programData = new TrainingProgram
            {
                Name = name,
                Level = level,
                Description = description,
                CategoryId = categoryId,
                InstructorId = instructorId
            };

            this.data.Programs.Add(programData);
            this.data.SaveChanges();

            return programData.Id;
        }

        private static IEnumerable<ProgramServiceModel> GetPrograms(IQueryable<TrainingProgram> programQuery)
         => programQuery
             .Select(p => new ProgramServiceModel
             {
                 Name = p.Name,
                 Level = p.Level,
                 Description = p.Description,
                 CategoryId = p.CategoryId
             })
             .ToList();

        
    }
}
