namespace FitnessShopSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Delivery;

    public class Delivery
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CustomerFirstNameMaxLength)]
        public string CustomerFirstName { get; set; }

        [Required]
        [MaxLength(CustomerLastNameMaxLength)]
        public string CustomerLastName { get; set; }

        [Required]
        [MaxLength(CompanyNameMaxLength)]
        public string Company { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        public int PostalCode { get; set; }

        [Required]
        [MaxLength(CityNameMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(CountryNameMaxlength)]
        public string Country { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }

        public int ProductId { get; set; }

    }
}
