using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Configurations;
using FeedTheCrowd.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace feedthecrowd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();
            var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<FeedTheCrowdContext>();
            //DataSeeder.Seed(context);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
