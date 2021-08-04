﻿namespace FitnessShopSystem.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
