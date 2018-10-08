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
    public class FrontendUserControllerDbTest
    {
        private static ServiceProvider _serviceCollection;
        private static readonly string _dbPathName = "test.sqlite";

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            var services = new ServiceCollection();
            services.AddTransient<IFrontendUserRepository, FrontendUserRepository>();
            services.AddDbContext<MyDbContext>(options => options.UseLazyLoadingProxies().UseSqlite("DataSource=" + _dbPathName, x => x.MigrationsAssembly("WgWall.Migrations")));

            _serviceCollection = services.BuildServiceProvider();

            //migrate db
            var context = _serviceCollection.GetService<MyDbContext>();
            context.Database.Migrate();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            File.Delete(_dbPathName);
        }

        [TestMethod]
        public async Task GetFrontendUser_ShouldNotFindFrontendUser()
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
    }
}
