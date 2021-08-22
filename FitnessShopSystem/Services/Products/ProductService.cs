namespace FitnessShopSystem.Services.Products
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using FitnessShopSystem.Data;
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Models.Products;
    using FitnessShopSystem.Services.Products.Models;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class ProductService : IProductService
    {
        private readonly FitnessShopDbContext data;
        private readonly IMapper mapper;

        public ProductService(FitnessShopDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

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
                ProductSorting.PriceAscending => productsQuery.OrderBy(p => p.Price),
                ProductSorting.PriceDescending => productsQuery.OrderByDescending(p => p.Price),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = GetProducts(productsQuery
                .Skip((currentPage - 1) * ProductSearchQueryModel.ProductsPerPage)
                .Take(ProductSearchQueryModel.ProductsPerPage));


            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                CurrentPage = currentPage,
                Products = products,
            };
        }

        public ProductDetailsServiceModel Details(int productId)
         => this.data
            .Products
            .Where(p => p.Id == productId)
            .ProjectTo<ProductDetailsServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefault();

        public async Task CreateAsync(string name, string brand, decimal price, string description, string flavour, string imageUrl, int categoryId, int manufacturerId)
        {
            var productData = new Product
            {
                Name = name,
                Brand = brand,
                Price = price,
                Description = description,
                Flavour = flavour,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                ManufacturerId = manufacturerId
            };

            await this.data.Products.AddAsync(productData);
            await this.data.SaveChangesAsync();

        }

        public bool Edit(int id, string name, string brand, decimal price, string description, string flavour, string imageUrl, int categoryId, int manufacturerId)
        {
            var product = this.data.Products.Find(id);

            if (product == null)
            {
                return false;
            }

            product.Name = name;
            product.Brand = brand;
            product.Price = price;
            product.Description = description;
            product.Flavour = flavour;
            product.ImageUrl = imageUrl;
            product.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public bool CategoryExist(int categoryId)
            => this.data
                .Categories
                .Any(c => c.Id == categoryId);

        public IEnumerable<ProductServiceModel> ByUser(string userId)
            => GetProducts(this.data
                .Products
                .Where(p => p.Manufacturer.UserId == userId));

        private static IEnumerable<ProductServiceModel> GetProducts(IQueryable<Product> productQuery)
            => productQuery
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Description = p.Description,
                    Flavour = p.Flavour,
                    ImageUrl = p.ImageUrl,
                    CategoryName = p.Category.Name,
                    CategoryId = p.CategoryId
                })
                .ToList();

        public IEnumerable<ProductCategoryServiceModel> AllProductCategories()
             => this.data
                .Categories
                .Select(p => new ProductCategoryServiceModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

        public async Task DeleteAsync(int id)
        {
            var product = this.data.Products.Find(id);

            this.data.Products.Remove(product);
            await this.data.SaveChangesAsync();
        }

        public IEnumerable<string> AllProductBrands()
            => this.data
                .Products
                .Select(p => p.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public int TotalProducts()
            => this.data.Products.Count();

        public IEnumerable<LatestProductsServiceModel> Latest()
            => this.data
                .Products
                .OrderByDescending(p => p.Id)
                .ProjectTo<LatestProductsServiceModel>(this.mapper.ConfigurationProvider)
                .Take(3)
                .ToList();
    }
}
