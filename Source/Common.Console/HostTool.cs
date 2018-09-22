using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zhoubin.Infrastructure.Common.Console
{
    /// <summary>
    /// 控制台启动应用工具
    /// </summary>
    public static class HostTool
    {
        /// <summary>
        /// 使用方法
        ///public static async Task Main(string[] args)
        ///{
        ///    await HostTool.CreateHostBuilder<T>(args);
        ///}
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task CreateHostBuilder<T>(string[] args) where T : class, IHostedService
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }

                    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var configFile = "appsettings.json";
                    if (string.IsNullOrEmpty(environmentName))
                    {
                        configFile = string.Format("appsettings.{0}.json", environmentName);
                    }
                    config.AddJsonFile(configFile, optional: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    //services.Configure<AppConfig>(hostContext.Configuration.GetSection("AppConfig"));

                    services.AddSingleton<IHostedService, T>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }

    public interface IHostedService<TSetting>: IHostedService,IDisposable
    {

    }
}
