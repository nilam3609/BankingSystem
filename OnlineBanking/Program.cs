using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineBanking.Repository;
using System;

namespace OnlineBanking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using(var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                SetupDB(services);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetupDB(IServiceProvider services)
        {
            using(var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using(var context = scope.ServiceProvider.GetRequiredService<BankContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}