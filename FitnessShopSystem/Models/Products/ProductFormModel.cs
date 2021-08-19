namespace FitnessShopSystem.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessShopSystem.Services.Products.Models;

    using static Data.DataConstants.Product;

    public class ProductFormModel
    {
        public int Id { get; set; }

        public int MyProperty { get; set; }
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; set; }

        [Range(PriceMinValue,PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Url]
        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(FlavourMaxLength, MinimumLength = FlavourMinLength)]
        public string Flavour { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<ProductCategoryServiceModel> Categories { get; set; }
    }
}
