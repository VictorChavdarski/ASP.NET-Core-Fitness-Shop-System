namespace FitnessShopSystem.Tests.Mocks
{
    using System;
    using FitnessShopSystem.Data;
    using Microsoft.EntityFrameworkCore;

    public static class DatabaseMock
    {
        public static FitnessShopDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<FitnessShopDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new FitnessShopDbContext(dbContextOptions);
            }
        }
    }
}
