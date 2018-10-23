using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WgWall.Data.Repository;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Test.Utils.Startup
{
    public class MockRepositoriesStartup : WgWall.Startup
    {
        public MockRepositoriesStartup(IConfiguration configuration) : base(configuration)
        {
        }
        
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFrontendUserRepository, FrontendUserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductPurchaseRepository, ProductPurchaseRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ITaskExecutionRepository, TaskExecutionRepository>();
            services.AddScoped<ITaskTemplateRepository, TaskTemplateRepository>();
            services.AddScoped<IEventRepository, EventRepository>();

            //add prod stuff
            base.ConfigureServices(services);
        }
    }
}
