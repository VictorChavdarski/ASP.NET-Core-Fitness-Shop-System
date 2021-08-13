namespace FitnessShopSystem.Models.Products
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Flavour { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
