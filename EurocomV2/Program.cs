using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Layer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EurocomV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            APICaller.InitializeClient();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
