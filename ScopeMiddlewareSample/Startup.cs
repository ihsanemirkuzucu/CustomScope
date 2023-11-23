using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScopeMiddlewareSample.Models;
using ScopeMiddlewareSample.Models.DbContext;
using ScopeMiddlewareSample.Models.Entity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopeMiddlewareSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        [Obsolete]
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSingleton<ConnectionHelper>();
            services.AddTransient<MyTransientEntity>();
            services.AddScoped<MyScopeEntity>();
            services.AddScoped<LifeTimeEx>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
           .WriteTo.MSSqlServer(
               connectionString: "Server=.;database=Kargo;Trusted_Connection=true;TrustServerCertificate=True",
               tableName: "Logs",
               autoCreateSqlTable: true)
           .CreateLogger();

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
