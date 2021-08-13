namespace FitnessShopSystem.Services.Products
{
    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public string Name { get; set; }

        public string Flavour { get; set; }

        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; }
    }
}
