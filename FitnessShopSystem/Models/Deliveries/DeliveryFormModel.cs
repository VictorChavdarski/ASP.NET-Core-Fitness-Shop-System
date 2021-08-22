namespace FitnessShopSystem.Models.Deliveries
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Services.Products.Models;
    using static Data.DataConstants.Delivery;

    public class DeliveryFormModel
    {
        [Required]
        [StringLength(CustomerFirstNameMaxLength, MinimumLength = CustomerFirstNameMinLength)]
        public string CustomerFirstName { get; set; }

        [Required]
        [StringLength(CustomerLastNameMaxLength, MinimumLength = CustomerLastNameMinLength)]
        public string CustomerLastName { get; set; }

        [Required]
        [StringLength(CompanyNameMaxLength, MinimumLength = CompanyNameMinLength)]
        public string Company { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; }

        [Required]
        public int PostalCode { get; set; }

        [Required]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        public string City { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(CountryNameMaxlength, MinimumLength = CountryNameMinLength)]
        public string Country { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string Phone { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }
    }
}
