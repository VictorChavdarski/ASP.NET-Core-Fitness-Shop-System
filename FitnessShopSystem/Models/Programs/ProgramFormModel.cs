namespace FitnessShopSystem.Models.Programs
{
    using FitnessShopSystem.Services.Programs;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.TrainingProgram;

    public class ProgramFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }
       
        public string Level { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<ProgramCategoryServiceModel> Categories { get; set; }
    }
}
