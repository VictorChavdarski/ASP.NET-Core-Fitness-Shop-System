namespace FitnessShopSystem.Models.Home
{
    using FitnessShopSystem.Services.Products.Models;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalProducts { get; set; }

        public int TotalUsers { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<LatestProductsServiceModel> Products { get; set; }
    }
}
