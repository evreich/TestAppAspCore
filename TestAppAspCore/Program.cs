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
        private static async void InitDB(IServiceProvider service)
        {
            BooksAndGenresSeedData.EnsurePopulated(service);
            await IdentitySeedData.EnsureRoles(service);
            await IdentitySeedData.EnsureMenuElements(service);
            await IdentitySeedData.EnsurePopulated(service);
        }

        public static void Main(string[] args)
        {
            var host = BuildWebHost(args).MigrateDatabase();
            InitDB(host.Services);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();

    }

}
