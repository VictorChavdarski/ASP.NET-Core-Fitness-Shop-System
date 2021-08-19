﻿namespace FitnessShopSystem.Services.Programs
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using FitnessShopSystem.Data;
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Models.Programs;
    using FitnessShopSystem.Services.Programs.Models;
  
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class ProgramService : IProgramService
    {
        private readonly FitnessShopDbContext data;
        private readonly IMapper mapper;

        public ProgramService(FitnessShopDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
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

        public int Create(string name, string level, string description,string imageUrl, int categoryId, int instructorId)
        {
            var programData = new TrainingProgram
            {
                Name = name,
                Level = level,
                Description = description,
                ImageUrl= imageUrl,
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
                 Id = p.Id,
                 Name = p.Name,
                 Level = p.Level,
                 Description = p.Description,
                 ImageUrl = p.ImageUrl,
                 CategoryId = p.CategoryId
             })
             .ToList();

        public ProgramDetailsServiceModel Details(int programId)
        => this.data
            .Programs
            .Where(p => p.Id == programId)
            .ProjectTo<ProgramDetailsServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefault();

        public bool Edit(int id, string name, string description, string level, string imageUrl, int categoryId, int instructorId)
        {
            var program = this.data.Programs.Find(id);

            if (program.InstructorId != instructorId)
            {
                return false;
            }

            if (program == null)
            {
                return false;
            }

            program.Name = name;
            program.Description = description;
            program.Level = level;
            program.ImageUrl = imageUrl;
            program.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var program = this.data.Programs.Find(id);

            this.data.Programs.Remove(program);
            await this.data.SaveChangesAsync();
        }
    }
}
