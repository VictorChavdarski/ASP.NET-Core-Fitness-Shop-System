namespace FitnessShopSystem.Infrastructure
{
    using AutoMapper;

    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Models.Products;
    using FitnessShopSystem.Models.Programs;
    using FitnessShopSystem.Services.Products.Models;
    using FitnessShopSystem.Services.Programs.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ProductDetailsServiceModel, ProductServiceModel>();
            this.CreateMap<Product, LatestProductsServiceModel>();
            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();
            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Manufacturer.UserId));

            this.CreateMap<TrainingProgram, LatestProgramsServiceModel>();
            this.CreateMap<ProgramDetailsServiceModel, ProgramFormModel>();
            this.CreateMap<TrainingProgram, ProgramDetailsServiceModel>()
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Instructor.UserId));
        }
    }
}
