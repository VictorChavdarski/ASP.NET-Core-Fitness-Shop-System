namespace FitnessShopSystem.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddProductFormModel
    {
        [Required]
        [StringLength(ProductBrandMaxLength, MinimumLength = ProductBrandMinLength)]
        public string Brand { get; init; }

        [Range(ProductPriceMinValue,ProductPriceMaxValue)]
        public decimal Price { get; init; }

        [Required]
        [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength)]
        public string Description { get; init; }

        [Url]
        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }
    }
}
