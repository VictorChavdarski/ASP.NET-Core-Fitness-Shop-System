namespace FitnessShopSystem.Services.Products
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using FitnessShopSystem.Models.Products;
    using FitnessShopSystem.Services.Products.Models;

    public interface IProductService
    {
        ProductQueryServiceModel All(string brand,
            string searchTerm,
            ProductSorting sorting,
            int currentPage);

        ProductDetailsServiceModel Details(int productId);

        Task DeleteAsync(int id);

        Task CreateAsync(
            string name,
            string brand,
            decimal price,
            string description,
            string flavour,
            string imageUrl,
            int categoryId,
            int manufacturerId);

        bool Edit(
            int id,
            string name,
            string brand,
            decimal price,
            string description,
            string flavour,
            string imageUrl,
            int categoryId,
            int manufacturerId);

        public IEnumerable<ProductServiceModel> ByUser(string userId);

        public IEnumerable<string> AllProductBrands();

        public IEnumerable<ProductCategoryServiceModel> AllProductCategories();

        int TotalProducts();

        bool CategoryExist(int categoryId);
    }
}
