namespace FitnessShopSystem.Services.Programs
{
    using System.Collections.Generic;

    using FitnessShopSystem.Models.Programs;
    using FitnessShopSystem.Services.Programs.Models;

    public interface IProgramService
    {
        ProgramQueryServiceModel All(string name,
         string searchTerm,
         ProgramSorting sorting,
         int currentPage);

        ProgramDetailsServiceModel Details(int programId);

        int Create(
            string name,
            string level,
            string description,
            string imageUrl,
            int categoryId,
            int instructorId);

        bool Edit(
           int id,
           string name,
           string description,
           string level,
           string imageUrl,
           int categoryId,
           int instructorId);

        public IEnumerable<ProgramServiceModel> ByUser(string userId);

        public IEnumerable<ProgramCategoryServiceModel> AllProgramCategories();

        bool CategoryExist(int categoryId);
    }
}
