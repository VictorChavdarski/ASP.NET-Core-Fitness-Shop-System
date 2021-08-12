namespace FitnessShopSystem.Tests.Mocks
{
    using Moq;
    using AutoMapper;

    public static class MapperMock 
    {
        public static IMapper Instance
        {
            get
            {
                var mapperMock = new Mock<IMapper>();

                mapperMock.SetupGet(m => m.ConfigurationProvider)
                    .Returns(Mock.Of<IConfigurationProvider>());

                return mapperMock.Object;
            }
        }
    }
}
