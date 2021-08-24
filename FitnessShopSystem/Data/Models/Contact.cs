namespace FitnessShopSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Contact;

    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(CountryNameMaxLength)]
        public string Country { get; set; }

        [Required]
        [MaxLength(SubjectMaxLength)]
        public string Subject { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
