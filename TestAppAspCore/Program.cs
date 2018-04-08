using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestAppAspCore.EFCore;
using TestAppAspCore.Infrastructure;
using TestAppAspCore.SeedDBHelpers;

namespace TestAppAspCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args).MigrateDatabase();
            
            //заполнение БД в случае отсутствия данных
            BooksAndGenresSeedData.EnsurePopulated(host.Services);
            IdentitySeedData.EnsureRoles(host.Services);
            IdentitySeedData.EnsurePopulated(host.Services);

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();

    }

}
