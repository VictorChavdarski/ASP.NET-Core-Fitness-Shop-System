namespace FitnessShopSystem.Services.Products
{
    using System.Linq;
    using FitnessShopSystem.Data;
    using FitnessShopSystem.Models.Products;

    public class ProductService : IProductService
    {
        private readonly FitnessShopDbContext data;

        public ProductService(FitnessShopDbContext data) 
            => this.data = data;

        public ProductQueryServiceModel All(string brand, string searchTerm, ProductSorting sorting, int currentPage)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                productsQuery = productsQuery.Where(p => p.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                       p.Brand.ToLower().Contains(searchTerm.ToLower()) ||
                       p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.Price => productsQuery.OrderBy(p => p.Price),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((currentPage - 1) * ProductSearchQueryModel.ProductsPerPage)
                .Take(ProductSearchQueryModel.ProductsPerPage)
                .Select(p => new ProductServiceModel
                {
                    Brand = p.Brand,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                })
                .ToList();

            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                CurrentPage = currentPage,
                Products = products
            };
        }
    }
}
