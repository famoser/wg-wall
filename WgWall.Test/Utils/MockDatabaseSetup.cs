using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WgWall.Data;
using WgWall.Test.Utils.SampleData;
using WgWall.Test.Utils.SampleData.Interface;

namespace WgWall.Test.Utils
{
    public class SeededDatabaseStartup : WgWall.Startup
    {
        public static string DbName = "db.sqlite";
        public SeededDatabaseStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            //ensure test db is removed
            if (File.Exists(DbName))
            {
                File.Delete(DbName);
            }

            //add test db to apply migrations afterwards
            var connection = @"Data Source = " + DbName;
            services.AddDbContext<MyDbContext>(options => options.UseLazyLoadingProxies().UseSqlite(connection, x => x.MigrationsAssembly("WgWall.Migrations")));
            services.AddScoped<ISampleDataService, SampleDataRepository>();

            //add prod stuff
            base.ConfigureServices(services);
        }

        public override void PreConfigureHook(IServiceProvider serviceScope)
        {
            //migrate
            base.PreConfigureHook(serviceScope);

            //seed db
            var sampleDataService = serviceScope.GetService<ISampleDataService>();
            var context = serviceScope.GetService<MyDbContext>();
            context.AddRange(sampleDataService.LoadEntities());
            context.SaveChanges(); ;
        }
    }
}