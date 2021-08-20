namespace FitnessShopSystem.Services.Programs.Models
{
    using System.Collections.Generic;

    public class ProgramQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int ProgramsPerPage { get; set; }

        public int TotalPrograms { get; set; }

        public IEnumerable<string> Levels { get; set; }

        public IEnumerable<ProgramServiceModel> Programs { get; set; }
    }
}
