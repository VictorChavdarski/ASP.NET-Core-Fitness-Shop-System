namespace FitnessShopSystem.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using FitnessShopSystem.Data;
    using FitnessShopSystem.Models.Products;
    using FitnessShopSystem.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using FitnessShopSystem.Infrastructure;

    public class ProductsController : Controller
    {
        private readonly FitnessShopDbContext data;

        public ProductsController(FitnessShopDbContext data)
            => this.data = data;

        public IActionResult All([FromQuery] ProductSearchQueryModel query)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                productsQuery = productsQuery.Where(p => p.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                       p.Brand.ToLower().Contains(query.SearchTerm.ToLower()) ||
                       p.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            productsQuery = query.Sorting switch
            {
                ProductSorting.Price => productsQuery.OrderBy(p => p.Price),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((query.CurrentPage - 1) * ProductSearchQueryModel.ProductsPerPage)
                .Take(ProductSearchQueryModel.ProductsPerPage)
                .Select(p => new ProductListingViewModel
                {
                    Brand = p.Brand,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                })
                .ToList();

            var productBrands = this.data
                .Products
                .Select(p => p.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

            query.TotalProducts = totalProducts;
            query.Brands = productBrands;
            query.Products = products;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsManufacturer())
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }

            return View(new AddProductFormModel
            {
                Categories = this.GetProductCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddProductFormModel product)
        {
            if (!this.UserIsManufacturer())
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }

            var manufacturerId = this.data
                .Manufacturers
                .Where(m => m.UserId == this.User.GetId())
                .Select(m => m.Id)
                .FirstOrDefault();

            if (manufacturerId == 0)
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }


            if (!this.data.Categories.Any(c => c.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.GetProductCategories();

                return View(product);
            }

            var productData = new Product
            {
                Brand = product.Brand,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                ManufacturerId = manufacturerId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsManufacturer()
            => this.data
                .Manufacturers
                .Any(m => m.UserId == this.User.GetId());

        private IEnumerable<ProductCategoryViewModel> GetProductCategories()
        => this.data
            .Categories
            .Select(p => new ProductCategoryViewModel
            {
                Id = p.Id,
                Name = p.Name
            })
            .ToList();
    }
}
