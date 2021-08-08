namespace FitnessShopSystem.Services.Products
{
    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public int ManufacturerId { get; init; }

        public string ManufacturerName { get; init; }

        public string UserId { get; init; }

        public int CategoryId { get; init; }
    }
}
