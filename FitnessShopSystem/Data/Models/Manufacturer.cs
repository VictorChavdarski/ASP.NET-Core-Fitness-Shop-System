namespace FitnessShopSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using static DataConstants.Manufacturer;

    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(CompanyNameMaxLength)]
        public string Company { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
