using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using quadra_ifsc.Logs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quadra_ifsc.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfiguracaoLogsQuadraIfsc.ConfigurarEscritaLogs();

            Log.Logger.Information("Iniciando o servidor da aplicação quadra-ifsc...");

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exc)
            {
                Log.Logger.Fatal(exc, "O servidor da aplicação quadra-ifsc parou inesperadamente.");
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
