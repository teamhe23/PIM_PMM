
using Data.PIM.Repository;
using Domain.Model.Properties;
using Domain.Repository.Oracle;
using Domain.Repository.PIM;
using Domain.Service;
using Job.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Util
{
    public static class IoCBootstrapper
    {
        public static IServiceCollection Bootstrap()
        {
            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", true, true)
                         .AddEnvironmentVariables()
                         .Build();

            services.AddLogging(log => log.AddConsole());

            services.Configure<OracleProperties>(info => config.GetSection("DB").Bind(info));
            //services.Configure<Settigns>(info => config.GetSection("Settigns").Bind(info));
            //services.Configure<ApiWMSProperties>(info => config.GetSection("api:wms").Bind(info));

            services.AddSingleton<IPrinterService, PrinterService>();
            //services.AddTransient<ISettignsService, SettignsService>();
            services.AddTransient<IPIMService, PIMService>();

            services.AddSingleton<IProductPIMRepository, ProductPIMRepository>();
            //services.AddSingleton<ILogsRepository, LogsRepository>();
            //services.AddSingleton<IProveedorRepository, ProveedorRepository>();
            //services.AddSingleton<IProveedorWMSRepository, ProveedorlWMSRepository>();


            return services;
        }
    }
}