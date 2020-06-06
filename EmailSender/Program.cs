using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailSender;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

namespace ToDoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(new RenderedCompactJsonFormatter(), "/logs/apilog.json")
            .CreateLogger();

            try {
                Log.Information("Application starting up");
                CreateHostBuilder(args).Build().Run();
            } catch (Exception ex)
            {
                Log.Fatal(ex, "Application startup failed");
            } finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
