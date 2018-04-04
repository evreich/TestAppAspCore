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
            string identityConnection = Configuration.GetConnectionString("IdentityConnection");

            services.AddLogging();
            services.AddDbContext<BooksContext>(options => options.UseSqlServer(defaultConnection));
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(identityConnection));
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
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opts =>
            {
                opts.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                opts.LoginPath = $"/{nameof(AccountController).Replace("Controller", "")}/{nameof(AccountController.Login)}";
                opts.AccessDeniedPath = $"/{nameof(AccountController).Replace("Controller", "")}/{nameof(AccountController.AccessDenied)}";
            });

            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IGenresRepository, GenresRepository>();

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
            app.UseAuthentication();
            app.UseStatusCodePagesWithReExecute("/Home/ErrorStatusCode", "?code={0}");

            app.UseStaticFiles();
            app.UseSession();

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

            //заполнение БД в случае отсутствия данных
            BooksAndGenresSeedData.EnsurePopulated(app);
            IdentitySeedData.EnsureRoles(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
