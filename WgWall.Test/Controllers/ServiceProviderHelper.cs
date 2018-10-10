using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WgWall.Data;
using WgWall.Data.Model;
using WgWall.Data.Repository;
using WgWall.Data.Repository.Interfaces;
using WgWall.Test.Mock.Data.Repositories;

namespace WgWall.Test.Controllers
{
    public class ServiceProviderHelper
    {
        private static readonly string _dbPathName = "test.sqlite";
        
        public static ServiceProvider SetUpMockDb()
        {
            var services = new ServiceCollection();
            services.AddTransient<IFrontendUserRepository, FrontendUserRepository>();
            services.AddDbContext<MyDbContext>(options => options.UseLazyLoadingProxies().UseSqlite("DataSource=" + _dbPathName, x => x.MigrationsAssembly("WgWall.Migrations")));

            var serviceProvider = services.BuildServiceProvider();

           PrepareDatabase(serviceProvider.GetService<MyDbContext>());

            return serviceProvider;
        }

        public static void PrepareDatabase(MyDbContext context)
        {
            //migrate
            context.Database.Migrate();
            context.EnsureSeeded();
        }
        
        public static void CleanupMockDb()
        {
            File.Delete(_dbPathName);
        }

        public static ServiceProvider SetUpMockRepositories()
        {
            var services = new ServiceCollection();
            services.AddTransient<IFrontendUserRepository>(provider => new MockFrontendUserRepository(SampleData.LoadFrontendUsers()));

            return services.BuildServiceProvider();
        }
    }
}
