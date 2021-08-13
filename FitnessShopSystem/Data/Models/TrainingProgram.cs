namespace FitnessShopSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.TrainingProgram;

    public class TrainingProgram
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string Level { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int InstructorId { get; init; }

        public Instructor Instructor { get; init; }
    }
}
