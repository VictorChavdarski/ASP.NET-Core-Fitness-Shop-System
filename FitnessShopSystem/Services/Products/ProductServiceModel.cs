namespace FitnessShopSystem.Services.Products
{
    public class ProductServiceModel
    {
        public int Id { get; init; }

        public string Brand { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
