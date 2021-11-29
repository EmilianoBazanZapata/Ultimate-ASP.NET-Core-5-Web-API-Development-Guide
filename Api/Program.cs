using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //inicializando el logger
            Log.Logger = new LoggerConfiguration()
            //ubicacion del log
            .WriteTo.File(path: "c:\\hotellistings\\logs\\log-.txt",
                          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                          //cantidad de veces que se creara el log
                          rollingInterval: RollingInterval.Day,
                          restrictedToMinimumLevel: LogEventLevel.Information
            ).CreateLogger();
             //al iniciar si es posible dentro del logger agregare la siguiente nota
            try
            {
                Log.Information("Aplication Is Starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //en cambioo si hay algun problema quedara registrado
                Log.Fatal(ex,"Application Failed to Start");
            }
            finally
            {
                //por ultimo cerramos el log
                Log.CloseAndFlush();
            }
            CreateHostBuilder(args).Build().Run();
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
