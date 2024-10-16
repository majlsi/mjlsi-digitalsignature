using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CommandLineApplication commandLineApplication = new CommandLineApplication(false);
            CommandOption doMigrate = commandLineApplication.Option(
                "--ef-migrate",
                "Apply entity framework migrations and exit",
                CommandOptionType.NoValue);
            var verifyMigrate = commandLineApplication.Option(
                "--ef-migrate-check",
                "Check the status of entity framework migrations",
                CommandOptionType.NoValue);
            commandLineApplication.HelpOption("-? | -h | --help");
            commandLineApplication.OnExecute(() =>
            {
                ExecuteApp(args, doMigrate, verifyMigrate);
                return 0;
            });
            commandLineApplication.Execute(args);

           // CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment != null)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();
                int port = config.GetValue<int>("port");
                IHostBuilder host = Host.CreateDefaultBuilder(args)
               .ConfigureLogging((hostingContext, config) => { config.ClearProviders(); })
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.ConfigureKestrel(options =>
                   {
                   // Set properties and call methods on options
                   options.Limits.MaxConcurrentConnections = 100;
                       options.Limits.MaxConcurrentUpgradedConnections = 100;
                       options.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100,
                           gracePeriod: TimeSpan.FromSeconds(10));
                       options.Limits.MinResponseDataRate = new MinDataRate(bytesPerSecond: 100,
                           gracePeriod: TimeSpan.FromSeconds(10));
                       options.Limits.MaxRequestBodySize = null;
                       options.Listen(IPAddress.Any, port);
                   });
                   webBuilder.UseStartup<Startup>();
               }).UseSerilog();

                return host;
            }
            else
            {
                Console.WriteLine("Fatal error: environment not found!");
                Environment.Exit(-1);
                return null;
            }
        }

        private static void ExecuteApp(string[] args, CommandOption doMigrate, CommandOption verifyMigrate)
        {
            Console.WriteLine("Loading web host");
            //
            // Please note that this webHostBuilder below is from an older 
            // dotnet core version. Newer dotnet cores have a simplified version
            // Use that instead and just take the command line parsing stuff with you
            var webHost = CreateHostBuilder(args).Build();
            if (verifyMigrate.HasValue() && doMigrate.HasValue())
            {
                Console.WriteLine("ef-migrate and ef-migrate-check are mutually exclusive, select one, and try again");
                Environment.Exit(2);
            }

            if (verifyMigrate.HasValue())
            {
                Console.WriteLine("Validating status of Entity Framework migrations");
                using (var serviceScope = webHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<DBEntities>())
                    {
                        var pendingMigrations = context.Database.GetPendingMigrations();
                        var migrations = pendingMigrations as IList<string> ?? pendingMigrations.ToList();
                        if (!migrations.Any())
                        {
                            Console.WriteLine("No pending migratons");
                            Environment.Exit(0);
                        }

                        Console.WriteLine("Pending migratons {0}", migrations.Count());
                        foreach (var migration in migrations)
                        {
                            Console.WriteLine($"\t{migration}");
                        }

                        Environment.Exit(3);
                    }
                }
            }

            if (doMigrate.HasValue())
            {
                Console.WriteLine("Applyting Entity Framework migrations");
                using (var serviceScope = webHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<DBEntities>())
                    {
                        context.Database.Migrate();
                        Console.WriteLine("All done, closing app");
                        Environment.Exit(0);
                    }
                }
            }

            // no flags provided, so just run the webhost
            webHost.Run();
        }
    }
}
