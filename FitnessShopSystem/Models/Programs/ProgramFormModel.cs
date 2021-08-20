namespace FitnessShopSystem.Models.Programs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessShopSystem.Services.Programs.Models;

    using static Data.DataConstants.TrainingProgram;

    public class ProgramFormModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }
       
        [Required]
        public string Level { get; set; }

        [Required]
        public string ImageUrl { get; set; }


    }
}
