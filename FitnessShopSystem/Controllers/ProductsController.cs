namespace FitnessShopSystem.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using FitnessShopSystem.Models.Products;
    using FitnessShopSystem.Infrastructure;
    using FitnessShopSystem.Services.Products;
    using FitnessShopSystem.Services.Manufacturers;
    using FitnessShopSystem.Services.Products.Models;

    using AutoMapper;

    public class ProductsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductService products;
        private readonly IManufacturerService manufacturers;

        public ProductsController(
            IMapper mapper,
            IProductService products,
            IManufacturerService manufacturers)
        {
            this.mapper = mapper;
            this.products = products;
            this.manufacturers = manufacturers;
        }

        public IActionResult All([FromQuery] ProductSearchQueryModel query)
        {
            var queryResult = this.products.All(query.Brand,query.SearchTerm,query.Sorting,query.CurrentPage);

            var productBrands = this.products.AllProductBrands();

            query.TotalProducts = queryResult.TotalProducts;
            query.Brands = productBrands;
            query.Products = queryResult.Products;

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
        public async Task<IActionResult> Add(ProductFormModel product)
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

            await this.products.CreateAsync(
               product.Name,
               product.Brand,
               product.Price,
               product.Description,
               product.Flavour,
               product.ImageUrl,
               product.CategoryId,
               manufacturerId);

            return RedirectToAction(nameof(Mine));
        }

        [Authorize]
        public IActionResult Buy(int id)
        {
            var product = this.products.Details(id);

            var productData = this.mapper.Map<ProductServiceModel>(product);

            productData.Categories = this.products.AllProductCategories();

            return View(productData);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var product = this.products.Details(id);

            if (product.UserId != this.User.GetId() && !User.IsAdmin())
            {
                return Unauthorized();
            }

            await this.products.DeleteAsync(id);

            return RedirectToAction(nameof(Mine));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.manufacturers.IsManufacturer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }

            var product = this.products.Details(id);

            if (product.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var productForm = this.mapper.Map<ProductFormModel>(product);

            productForm.Categories = this.products.AllProductCategories();

            return View(productForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, ProductServiceModel product)
        {
            if (!this.manufacturers.IsManufacturer(this.User.GetId()) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }

            var manufacturerId = this.manufacturers.GetId(this.User.GetId());

            if (manufacturerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManufacturesController.Create), "Manufactures");
            }

            if (!ModelState.IsValid)
            {
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

            if (User.IsAdmin())
            {
                return RedirectToAction("All", "Products");
            }

            return RedirectToAction(nameof(Mine));
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myProducts = this.products.ByUser(this.User.GetId());

            return View(myProducts);
        }
    }
}
