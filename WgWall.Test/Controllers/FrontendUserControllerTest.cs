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
        private static readonly string _dbPathName = "test.sqlite2";

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            var services = new ServiceCollection();
            services.AddTransient<IFrontendUserRepository, MockFrontendUserRepository>();
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
        public async Task GetProduct_ShouldNotFindProduct()
        {
            var controller = new FrontendUsersController(new FrontendUserRepository(_serviceCollection.GetService<MyDbContext>()));

            var result = await controller.Check("not contained");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            //var res = await result.ExecuteResultAsync()
        }
        private List<FrontendUser> GetTestUsers()
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
