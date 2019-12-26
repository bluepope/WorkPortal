using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BluePope.WorkPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options => {
                         int port = 5000;

                         string in_port = args?.FirstOrDefault(p => p.ToLower() == "--port=");
                         if (string.IsNullOrWhiteSpace(in_port) == false)
                         {
                             port = Convert.ToInt16(in_port.Replace("--port=", ""));
                         }

                         options.Limits.MaxRequestBodySize = 1000 * 1000 * 1024; //1GB; kestrel ���ε� �뷮 ����
                         options.ListenAnyIP(port);
                         try
                         {
                             options.ListenAnyIP(port + 1, (config) =>
                             {
                                 config.UseHttps();
                             });
                         }
                         catch { }
                     });
                });
    }
}
