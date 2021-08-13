namespace FitnessShopSystem.Services.Products
{
    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public string UserId { get; set; }
    }
}
