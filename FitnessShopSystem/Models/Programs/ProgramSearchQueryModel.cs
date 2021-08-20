namespace FitnessShopSystem.Models.Programs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProgramSearchQueryModel
    {
        public const int ProgramsPerPage = 6;

        public string Name { get; init; }

        public string Level { get; init; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPrograms { get; set; }

        public ProgramSorting Sorting { get; init; }

        public IEnumerable<string> Levels { get; set; }

        public IEnumerable<ProgramEditViewModel> Programs { get; set; }
    }
}
