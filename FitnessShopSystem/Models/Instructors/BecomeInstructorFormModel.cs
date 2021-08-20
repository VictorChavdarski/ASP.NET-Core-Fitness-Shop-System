namespace FitnessShopSystem.Models.Instructors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Instructor;

    public class BecomeInstructorFormModel
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string LastName { get; set; }

        [Range(18,60)]
        public int Age { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
