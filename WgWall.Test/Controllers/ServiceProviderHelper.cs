﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WgWall.Data;
using WgWall.Data.Model;
using WgWall.Data.Repository;
using WgWall.Data.Repository.Interfaces;
using WgWall.Test.Mock;
using WgWall.Test.Mock.Data.Repositories;

namespace WgWall.Test.Controllers
{
    public class ServiceProviderHelper
    {
        private static readonly string _dbPathName = "test.sqlite";
        
        public static ServiceProvider SetUpMockDb()
        {
            //fill up read services & mock DB
            var startup = new MockStartup(null);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);

            //prepare db for run
            var serviceProvider = services.BuildServiceProvider();
            startup.PrepareDatabase(serviceProvider.GetService<MyDbContext>());

            return serviceProvider;
        }
        
        public static void CleanupMockDb()
        {
            File.Delete(_dbPathName);
        }

        public static ServiceProvider SetUpMockRepositories()
        {
            var services = new ServiceCollection();
            services.AddTransient<IFrontendUserRepository>(provider => new MockFrontendUserRepository(SampleData.LoadFrontendUsers()));
            services.AddTransient<IProductRepository>(provider => new MockProductRepository(SampleData.LoadProducts()));
            services.AddTransient<ISettingRepository>(provider => new MockSettingRepository(SampleData.LoadSettings()));

            return services.BuildServiceProvider();
        }
    }
}
