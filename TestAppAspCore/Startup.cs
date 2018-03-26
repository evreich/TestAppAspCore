using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.EFCore;

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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddLogging();
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IGenresRepository, GenresRepository>();
            services.AddDbContext<BooksContext>(options => options.UseSqlServer(connection));
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "userBooksByGenrePagination",
                    template: "{area:exists}/Books/{genre}/Page/{page:int}",
                    defaults: new { area = "Market" , controller = "Home", action = "ShowBooksByGenre" }
                );

                routes.MapRoute(
                    name: "userIndexBooksPagination",
                    template: "{area:exists}/Books/Page/{page:int}",
                    defaults: new { area = "Market" , controller = "Home", action = "ShowBooks" }
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
                    defaults: new { controller="Home", action="Index" });

                routes.MapRoute(
                    name: "adminDefault",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
