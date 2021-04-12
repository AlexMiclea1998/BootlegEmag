using BootlegEmagService.Events;
using BootlegEmagService.Events.Repository;
using BootlegEmagService.Product;
using BootlegEmagService.Product.Repository;
using BootlegEmagService.ShoppingCart;
using BootlegEmagService.ShoppingCart.Repository;
using BootlegEmagService.ShoppingCart.Repository.DataStore;
using BootlegEmagService.User;
using BootlegEmagService.User.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            //Database
            services.Configure<DatabaseConfiguration>(Configuration.GetSection("DatabaseConfiguration"));

            //Shopping Cart
            services.AddSingleton<IShoppingCartCacheStrategy, InMemoryCacheStrategy>();
            services.AddSingleton<IShoppingCartDataStoreStrategy, SQLiteDataStoreStrategy>();
            services.AddSingleton<SQLiteReaderShoppingCartConverter, SQLiteReaderShoppingCartConverter>();
            services.AddSingleton<ShoppingCartRepository, ShoppingCartRepository>();
            services.AddSingleton<ShoppingCartFacade, ShoppingCartFacade>();

            //Event
            services.AddSingleton<EventRepository, EventRepository>();
            services.AddSingleton<EventFacade, EventFacade>();

            //User
            services.AddSingleton<UserRepository, UserRepository>();
            services.AddSingleton<UserFacade, UserFacade>();

            //Product
            services.AddSingleton<ProductRepository, ProductRepository>();
            services.AddSingleton<ProductFacade, ProductFacade>();
            
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
