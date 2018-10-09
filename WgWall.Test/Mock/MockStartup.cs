﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WgWall.Data;
using WgWall.Data.Repository;
using WgWall.Data.Repository.Interfaces;
using WgWall.Test.Controllers;

namespace WgWall.Test.Mock
{
    public class MockStartup : Startup
    {
        public static string DbName = "test.sqlite";
        public MockStartup(IConfiguration configuration) : base(configuration)
        {
        }
        
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            //remove prod db
            var context = services.FirstOrDefault(s => s.ServiceType == typeof(MyDbContext));
            services.Remove(context);

            //add test db to apply migrations afterwards
            var connection = @"Data Source = " + DbName;
            services.AddDbContext<MyDbContext>(options => options.UseLazyLoadingProxies().UseSqlite(connection, x => x.MigrationsAssembly("WgWall.Migrations")));
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ServiceProviderHelper.PrepareDatabase(app.ApplicationServices.GetService<MyDbContext>());
            base.Configure(app, env);
        }
    }
}
