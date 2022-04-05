using System;

using LionSkyNot.Data;

using Microsoft.EntityFrameworkCore;


namespace LionSkyNot.Tests.Mock
{
    public static class DatabaseMock
    {

        public static LionSkyDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<LionSkyDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new LionSkyDbContext(dbContextOptions);
            }
        }

    }
}
