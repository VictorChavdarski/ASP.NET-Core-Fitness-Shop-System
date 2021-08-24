namespace FitnessShopSystem.Models.Home
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Contact;

    public class ContactFormModel
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(CountryNameMaxLength, MinimumLength = CountryNameMinLength)]
        public string Country { get; set; }

        [Required]
        [StringLength(SubjectMaxLength, MinimumLength = SubjectMinLength)]
        public string Subject { get; set; }
    }
}
