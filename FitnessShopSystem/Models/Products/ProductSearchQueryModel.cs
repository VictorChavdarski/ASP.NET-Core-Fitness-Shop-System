namespace FitnessShopSystem.Models.Products
{
    using FitnessShopSystem.Services.Products;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductSearchQueryModel
    {
        public const int ProductsPerPage = 6;

        public string Brand { get; init; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalProducts { get; set; }

        public ProductSorting Sorting { get; init; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; }
    }
}
