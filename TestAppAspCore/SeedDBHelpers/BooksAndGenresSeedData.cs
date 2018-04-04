using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.EFCore;

namespace TestAppAspCore.SeedDBHelpers
{
    public static class BooksAndGenresSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            BooksContext context = serviceScopeFactory.CreateScope().ServiceProvider.GetService<BooksContext>();

            try
            {
                 if (!context.Genres.Any())
                {
                    DBGenresUpdater updater = new DBGenresUpdater(context);
                    updater.Init();
                }
                if (!context.Books.Any())
                {
                    DBBooksUpdater updater = new DBBooksUpdater(context);
                    updater.Init();
                }
            }
            catch (Exception ex)
            {
                var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
