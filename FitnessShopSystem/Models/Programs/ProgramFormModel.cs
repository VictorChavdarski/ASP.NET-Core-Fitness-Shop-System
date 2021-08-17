namespace FitnessShopSystem.Models.Programs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessShopSystem.Services.Programs.Models;

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

        public string ImageUrl { get; set; }


        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<ProgramCategoryServiceModel> Categories { get; set; }
    }
}
