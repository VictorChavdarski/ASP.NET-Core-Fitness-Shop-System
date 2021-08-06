namespace FitnessShopSystem.Services.Products
{
    using FitnessShopSystem.Models.Products;
    public interface IProductService
    {
        ProductQueryServiceModel All(string brand,
            string searchTerm,
            ProductSorting sorting,
            int currentPage);
    }
}
