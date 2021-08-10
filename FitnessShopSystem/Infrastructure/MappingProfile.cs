namespace FitnessShopSystem.Infrastructure
{
    using AutoMapper;
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Models.Home;
    using FitnessShopSystem.Models.Products;
    using FitnessShopSystem.Services.Products;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Product, ProductIndexViewModel>();
            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();
            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Manufacturer.UserId));
        }
    }
}
