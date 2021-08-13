﻿namespace FitnessShopSystem.Services.Products
{
    using System.Linq;
    using System.Collections.Generic;
    using FitnessShopSystem.Data;
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Models.Products;
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
                ProductSorting.Price => productsQuery.OrderBy(p => p.Price),
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
                Products = products
            };
        }

        public ProductDetailsServiceModel Details(int id)
            => this.data
                .Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Flavour = p.Flavour,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    ManufacturerId = p.ManufacturerId,
                    ManufacturerName = p.Manufacturer.Name,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    UserId = p.Manufacturer.UserId
                })
                .FirstOrDefault();

        public int Create(string name, string brand, decimal price, string description, string flavour, string imageUrl, int categoryId, int manufacturerId)
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

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return productData.Id;
        }

        public bool Edit(int id,string name, string brand, decimal price, string description, string flavour, string imageUrl, int categoryId, int manufacturerId)
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
                    Brand = p.Brand,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
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
    }
}