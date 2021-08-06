namespace FitnessShopSystem.Services.Products
{
    using System.Collections.Generic;

    public class ProductQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int ProductsPerPage { get; set; }

        public int TotalProducts { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; }
    }
}
