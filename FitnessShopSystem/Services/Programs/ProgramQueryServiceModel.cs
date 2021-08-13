namespace FitnessShopSystem.Services.Programs
{
    using System.Collections.Generic;

    public class ProgramQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int ProgramsPerPage { get; set; }

        public int TotalPrograms { get; set; }

        public IEnumerable<ProgramServiceModel> Programs { get; set; }
    }
}
