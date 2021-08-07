namespace FitnessShopSystem.Models.Manufactures
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Manufacturer;

    public class BecomeManufacturerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
