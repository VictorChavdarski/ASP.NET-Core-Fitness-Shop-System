namespace FitnessShopSystem.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using FitnessShopSystem.Data;
    using FitnessShopSystem.Models.Products;
    using Microsoft.AspNetCore.Authorization;
    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Services.Products;
    using FitnessShopSystem.Services.Manufacturers;
    using AutoMapper;

    public class ProductsController : Controller
    {
        private readonly FitnessShopDbContext data;
        private readonly IProductService products;
        private readonly IManufacturerService manufacturers;
        private readonly IMapper mapper;

        public ProductsController(
            FitnessShopDbContext data,
            IProductService products,
            IManufacturerService manufacturers,
            IMapper mapper)
        {
            this.data = data;
            this.products = products;
            this.manufacturers = manufacturers;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Buy(int id)
        {
            var product = this.data.Products.Where(p => p.Id == id)
                .Select(p => new ProductBuyViewModel
                {
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Description = p.Description,
                    Flavour = p.Flavour,
                    ImageUrl = p.ImageUrl,
                    CategoryName = p.Category.Name
                })
                .FirstOrDefault();

            return View(product);
        }

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
                .Select(p => new ProductServiceModel
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
            if (!this.manufacturers.IsManufacturer(this.User.GetId()))
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }

            return View(new ProductFormModel
            {
                Categories = this.products.AllProductCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ProductFormModel product)
        {
            var manufacturerId = this.manufacturers.GetId(this.User.GetId());

            if (manufacturerId == 0)
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }

            if (!this.products.CategoryExist(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllProductCategories();

                return View(product);
            }

            this.products.Create(
               product.Name,
               product.Brand,
               product.Price,
               product.Description,
               product.Flavour,
               product.ImageUrl,
               product.CategoryId,
               manufacturerId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myProducts = this.products.ByUser(this.User.GetId());

            return View(myProducts);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.manufacturers.IsManufacturer(userId))
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }

            var product = this.data
                            .Products
                            .Where(p => p.Id == id)
                            .Select(p => new ProductDetailsServiceModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Brand = p.Brand,
                                Price = p.Price,
                                Description = p.Description,
                                Flavour = p.Flavour,
                                ImageUrl = p.ImageUrl,
                                CategoryName = p.Category.Name,
                                CategoryId = p.CategoryId,
                                ManufacturerName = p.Manufacturer.Name,
                                ManufacturerId = p.ManufacturerId,
                            })
                            .FirstOrDefault();

            return View(product);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, ProductServiceModel product)
        {
            if (!this.manufacturers.IsManufacturer(this.User.GetId()))
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }

            var manufacturerId = this.manufacturers.GetId(this.User.GetId());

            if (manufacturerId == 0)
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }

            if (!this.products.CategoryExist(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllProductCategories();
                return View(product);
            }

            var editedProduct = this.products.Edit(
               id,
               product.Name,
               product.Brand,
               product.Price,
               product.Description,
               product.Flavour,
               product.ImageUrl,
               product.CategoryId,
               manufacturerId);

            if (!editedProduct)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
