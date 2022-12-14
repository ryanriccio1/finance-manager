using FinanceManager.FinancialModelAPI;
using FinanceManager.FinancialModelAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.HostBuilders
{
    public static class AddFinanceAPIHostBuilderExtensions
    {
        public static IHostBuilder AddFinanceAPI(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {   // we can setup our api either from an external config or from here directly
                string apiKey = context.Configuration.GetValue<string>("FINANCE_API_KEY");
                services.AddSingleton(new FinancialModelAPIKey(apiKey));
                services.AddHttpClient<FinancialModelAPIHttpClient>(c =>
                {
                    c.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
                });

            });

            return host;
        }
    }
}
