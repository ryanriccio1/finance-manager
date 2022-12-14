using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services;
using FinanceManager.Domain.Services.AuthenticationServices;
using FinanceManager.Domain.Services.TransactionServices;
using FinanceManager.EntityFramework.Services;
using FinanceManager.FinancialModelAPI.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {   // these bring in and process data for the application to use
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IDataService<Account>, AccountDataService>();
                services.AddSingleton<IAccountService, AccountDataService>();
                services.AddSingleton<IStockPriceService, StockPriceService>();
                services.AddSingleton<IBuyStockService, BuyStockService>();
                services.AddSingleton<ISellStockService, SellStockService>();
                services.AddSingleton<IMajorIndexService, MajorIndexService>();
                services.AddSingleton<IPasswordHasher, PasswordHasher>();
            });

            return host;
        }

    }
}
