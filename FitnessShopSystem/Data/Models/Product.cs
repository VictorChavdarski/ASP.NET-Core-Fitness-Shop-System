namespace FitnessShopSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Product
    {
        public int Id { get; init; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
