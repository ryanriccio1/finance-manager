using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.HostBuilders
{
    public static class AddConfigurationHostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host) 
        {   // get information about our host from both environment variable and a config file
            host.ConfigureAppConfiguration(c =>
            {
                c.AddJsonFile("appsettings.json");
                c.AddEnvironmentVariables();
            });

            return host;
        }
    }
}
