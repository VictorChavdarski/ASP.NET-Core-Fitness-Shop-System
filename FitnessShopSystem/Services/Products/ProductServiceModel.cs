using System.Collections.Generic;

namespace FitnessShopSystem.Services.Products
{
    public class ProductServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Flavour { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<ProductCategoryServiceModel> Categories { get; set; }
    }
}
