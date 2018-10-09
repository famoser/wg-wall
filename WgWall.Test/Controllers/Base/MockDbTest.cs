using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Controllers;
using WgWall.Data;
using WgWall.Data.Repository;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Test.Controllers.Base
{
    public abstract class MockDbTest
    {
        protected static ServiceProvider ServiceProvider;
        private static readonly string _dbPathName = "test.sqlite";

        [TestInitialize]
        public void SetUp()
        {
            var services = new ServiceCollection();
            services.AddTransient<IFrontendUserRepository, FrontendUserRepository>();
            services.AddDbContext<MyDbContext>(options => options.UseLazyLoadingProxies().UseSqlite("DataSource=" + _dbPathName, x => x.MigrationsAssembly("WgWall.Migrations")));

            ServiceProvider = services.BuildServiceProvider();

            //migrate db
            var context = ServiceProvider.GetService<MyDbContext>();
            context.Database.Migrate();
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(_dbPathName);
        }
    }
}
