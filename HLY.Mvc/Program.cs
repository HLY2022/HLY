using System;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HLY.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders(); //去掉默认的日志
                })
               .UseServiceProviderFactory(new AutofacServiceProviderFactory())   //将默认ServiceProviderFactory指定为AutofacServiceProviderFactory
                .ConfigureWebHostDefaults(webBuilder =>
            {
                var configuration = ConfigHelper.GetConfigRoot();
                var httpHost = configuration["AppSetting:HttpHost"];
                webBuilder.UseUrls(httpHost).UseStartup<Startup>();
                Console.WriteLine($"启动成功，访问地址:{httpHost}");
            });

    }
}
