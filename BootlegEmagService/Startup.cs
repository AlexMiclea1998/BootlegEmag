using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootlegEmagService.ShoppingCart;
using BootlegEmagService.ShoppingCart.Repository;
using BootlegEmagService.ShoppingCart.Repository.DataStore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BootlegEmagService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.Configure<ShoppingCartConfiguration>(Configuration.GetSection("SQLiteShoppingCartConfiguration"));
            services.AddSingleton<IShoppingCartCacheStrategy, InMemoryCacheStrategy>();
            services.AddSingleton<IShoppingCartDataStoreStrategy, SQLiteDataStoreStrategy>();
            services.AddSingleton<SQLiteReaderShoppingCartConverter, SQLiteReaderShoppingCartConverter>();
            services.AddSingleton<ShoppingCartRepository, ShoppingCartRepository>();
            services.AddSingleton<ShoppingCartFacade, ShoppingCartFacade>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
