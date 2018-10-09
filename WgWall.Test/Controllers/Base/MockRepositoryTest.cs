using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using WgWall.Test.Mock.Data.Repositories;

namespace WgWall.Test.Controllers.Base
{
    public abstract class MockRepositoryTest
    {
        protected static ServiceProvider ServiceProvider;
        
        [TestInitialize]
        public void SetUp()
        {
            var services = new ServiceCollection();
            services.AddTransient<IFrontendUserRepository>(provider => new MockFrontendUserRepository(GetTestUsers()));

            ServiceProvider = services.BuildServiceProvider();
        }

        private static List<FrontendUser> GetTestUsers()
        {
            var frontendUsers = new List<FrontendUser>
            {
                new FrontendUser {Name = "Florian"},
                new FrontendUser {Name = "Cédric"},
                new FrontendUser {Name = "Xenia"}
            };
            return frontendUsers;
        }
    }
}
