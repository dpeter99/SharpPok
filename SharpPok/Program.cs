using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SharpPok
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(kesterConfig =>
                    {
                        kesterConfig.Listen(IPAddress.Any, 80);
                        kesterConfig.Listen(IPAddress.Loopback, 15060);
                    });
                })
                .ConfigureAppConfiguration(Settings);
        }


        private static void Settings(HostBuilderContext env, IConfigurationBuilder arg2)
        {
            if(env.HostingEnvironment.IsDevelopment() || env.HostingEnvironment.EnvironmentName == "Design")
            {
                arg2.AddUserSecrets<Program>();
            }
        }
    }
}