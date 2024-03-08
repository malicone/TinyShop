using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Infrastructure;
using Serilog;

namespace TinyShop
{
    public class Program
    {
        public static void Main( string[] args )
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console().WriteTo.File( "Logs\\log.txt", rollingInterval: RollingInterval.Day )
                .CreateLogger();
            try
            {
                Log.Information( "Starting up" );
                CreateHostBuilder( args ).Build().Run();
            }
            catch ( Exception ex )
            {
                Log.Fatal( ex, "Application start-up failed" );
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder( string[] args ) =>
            Host.CreateDefaultBuilder( args )
                .UseSerilog()
                .ConfigureWebHostDefaults( webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 } );
    }
}
