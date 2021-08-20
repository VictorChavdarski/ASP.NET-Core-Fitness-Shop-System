namespace FitnessShopSystem.Services.Programs
{
    using System.Threading.Tasks;
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

        Task DeleteAsync(int id);

        Task CreateAsync(
            string name,
            string level,
            string description,
            string imageUrl,
            int instructorId);

        bool Edit(
           int id,
           string name,
           string description,
           string level,
           string imageUrl,
           int instructorId);

        public IEnumerable<string> AllProgramLevels();

        public IEnumerable<ProgramServiceModel> ByUser(string userId);
    }
}
