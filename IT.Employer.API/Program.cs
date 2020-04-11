using System;
using System.Threading.Tasks;
using IT.Employer.Domain;
using IT.Employer.Services.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IT.Employer.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            EnsureDatabaseInitialized(host);
            await host.StartAsync();
            Console.Out.WriteLine("API_IS_READY");
            await host.WaitForShutdownAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:6550");
                });

        public static void EnsureDatabaseInitialized(IHost host)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));

            using (var serviceScope = host.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                ItEmployerDbContext context = serviceScope.ServiceProvider.GetRequiredService<ItEmployerDbContext>();
                DatabaseInitializer.EnsureDatabaseInitialized(context);
            }
        }
    }
}
