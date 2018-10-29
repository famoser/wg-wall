using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WgWall.Data;
using WgWall.Data.Repository;
using WgWall.Data.Repository.Interfaces;

namespace WgWall
{
    public class Startup
    {
        public const string DbFileName = "db.sqlite";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            var connection = "Data Source = " + DbFileName;
            services.AddDbContext<MyDbContext>(options => options.UseLazyLoadingProxies().UseSqlite(connection, x => x.MigrationsAssembly("WgWall.Migrations")));
            services.AddScoped<IFrontendUserRepository, FrontendUserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductPurchaseRepository, ProductPurchaseRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ITaskExecutionRepository, TaskExecutionRepository>();
            services.AddScoped<ITaskTemplateRepository, TaskTemplateRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
        }

        public virtual void PreConfigureHook(IServiceProvider serviceScope)
        {
            //migrate
            serviceScope.GetService<MyDbContext>().Database.Migrate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                PreConfigureHook(serviceScope.ServiceProvider);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
