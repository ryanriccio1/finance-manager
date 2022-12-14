using FinanceManager.WPF.State.Accounts;
using FinanceManager.WPF.State.Assets;
using FinanceManager.WPF.State.Authenticators;
using FinanceManager.WPF.State.Navigators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {   // add stores that store the state of the application
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<IAccountStore, AccountStore>();
                services.AddSingleton<AssetStore>();
            });

            return host;
        }
    }
}
