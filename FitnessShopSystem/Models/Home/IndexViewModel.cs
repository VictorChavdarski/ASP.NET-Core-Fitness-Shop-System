namespace FitnessShopSystem.Models.Home
{
    using System.Collections.Generic;

    using FitnessShopSystem.Services.Products.Models;

    public class IndexViewModel
    {
        public int TotalProducts { get; set; }

        public int TotalUsers { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<LatestProductsServiceModel> Products { get; set; }
    }
}
