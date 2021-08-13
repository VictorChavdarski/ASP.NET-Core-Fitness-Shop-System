using FitnessShopSystem.Models.Programs;
using System.Collections.Generic;

namespace FitnessShopSystem.Services.Programs
{
    public interface IProgramService
    {
        ProgramQueryServiceModel All(string name,
         string searchTerm,
         ProgramSorting sorting,
         int currentPage);

       // ProductEditViewModel Details(int id);

        int Create(
            string name,
            string level,
            string description,
            int categoryId,
            int instructorId);

        public IEnumerable<ProgramServiceModel> ByUser(string userId);

        public IEnumerable<ProgramCategoryServiceModel> AllProgramCategories();

        bool CategoryExist(int categoryId);
    }
}
