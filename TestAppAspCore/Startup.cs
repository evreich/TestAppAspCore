using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAppAspCore.Areas.Market.Controllers;
using TestAppAspCore.Controllers;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.EFCore;
using TestAppAspCore.SeedDBHelpers;
using TestAppAspCore.Models;
using Microsoft.EntityFrameworkCore;
using QPD.PartialMenuLibrary;

namespace TestAppAspCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string defaultConnection = Configuration.GetConnectionString("DefaultConnection");

            services.AddLogging();
            services.AddDbContext<BooksContext>(options => options.UseSqlServer(defaultConnection), ServiceLifetime.Transient);
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 5; 
                opts.Password.RequireNonAlphanumeric = false;   
                opts.Password.RequireLowercase = false; 
                opts.Password.RequireUppercase = false; 
                opts.Password.RequireDigit = true;
                opts.Lockout.MaxFailedAccessAttempts = 5;
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                
            })
            .AddEntityFrameworkStores<BooksContext>()
            .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opts =>
            {
                opts.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                opts.LoginPath = "/Account/Login";
                opts.AccessDeniedPath = "/Account/AccessDenied";
            });

            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IGenresRepository, GenresRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IBookOrderRepository, BookOrderRepository>();

            services.AddScoped<MenuService<MenuForRole>>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/ServerError");
            }

            app.UseStatusCodePagesWithReExecute("/Home/ErrorStatusCode", "?code={0}");

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "userBooksByGenrePagination",
                    template: "{area:exists}/Books/{genre}/Page/{page:int}",
                    defaults: new { area = "Market", controller = "Home", action = "ShowBooksByGenre" }
                );

                routes.MapRoute(
                    name: "userIndexBooksPagination",
                    template: "{area:exists}/Books/Page/{page:int}",
                    defaults: new { area = "Market", controller = "Home", action = "ShowBooks" }
                );

                routes.MapRoute(
                    name: "userDefault",
                    template: "{area:exists}/{controller=Home}/{action=ShowBooks}/{id?}"
                );

                routes.MapRoute(
                    name: "adminGenres",
                    template: "Genres",
                    defaults: new { controller = "Home", action = "ShowGenres" }
                );

                routes.MapRoute(
                    name: "adminIndexBooksPagination",
                    template: "Books/Page/{page:int}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "adminDefault",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
