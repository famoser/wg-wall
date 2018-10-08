using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Controllers;
using WgWall.Data;
using WgWall.Data.Model;
using WgWall.Data.Repository;
using WgWall.Data.Repository.Interfaces;
using WgWall.Test.Mock.Data.Repositories;

namespace WgWall.Test.Controllers
{
    [TestClass]
    public class FrontendUserControllerTest
    {
        private static ServiceProvider _serviceCollection;
        private static readonly string _dbPathName = "test.sqlite";

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            var services = new ServiceCollection();
            services.AddTransient<IFrontendUserRepository>(provider => new MockFrontendUserRepository(GetTestUsers()));

            _serviceCollection = services.BuildServiceProvider();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            File.Delete(_dbPathName);
        }

        [TestMethod]
        public async Task GetFrontendUser_ShouldFindFrontendUser()
        {
            //arrange
            var controller = new FrontendUsersController(_serviceCollection.GetService<IFrontendUserRepository>());

            //act
            var result = await controller.Check("not contained");
            var objectResult = result as OkObjectResult;

            //assert
            Assert.IsNotNull(objectResult);
            Assert.IsInstanceOfType(objectResult.Value, typeof(bool));
            Assert.AreEqual(false, (bool)objectResult.Value);
        }

        private static List<FrontendUser> GetTestUsers()
        {
            var frontendUsers = new List<FrontendUser>
            {
                new FrontendUser {Name = "Florian"},
                new FrontendUser {Name = "C�dric"},
                new FrontendUser {Name = "Xenia"}
            };
            return frontendUsers;
        }
    }
}
