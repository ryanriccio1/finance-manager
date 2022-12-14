using FinanceManager.Domain.Services;
using FinanceManager.WPF.State.Authenticators;
using FinanceManager.WPF.State.Navigators;
using FinanceManager.WPF.ViewModels;
using FinanceManager.WPF.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {   
                // create the main view models
                services.AddSingleton(CreateHomeViewModel);
                services.AddSingleton<PortfolioViewModel>();
                services.AddSingleton<BuyViewModel>();
                services.AddSingleton<SellViewModel>();
                services.AddSingleton<AssetSummaryViewModel>();
                services.AddSingleton<MainViewModel>();

                // create lambda functions that we pass in later to create view models in our factory
                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services => () => services.GetRequiredService<PortfolioViewModel>());
                services.AddSingleton<CreateViewModel<BuyViewModel>>(services => () => services.GetRequiredService<BuyViewModel>());
                services.AddSingleton<CreateViewModel<SellViewModel>>(services => () => services.GetRequiredService<SellViewModel>());
                services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
                services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));

                // create our factory
                services.AddSingleton<IFinanceManagerViewModelFactory, FinanceManagerViewModelFactory>();

                // create renavigators for later
                services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
            });

            return host;
        }

        // these view models requires us to load more data when we instatiate them (no circular dependecies)
        private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
        {
            return new HomeViewModel(
                services.GetRequiredService<AssetSummaryViewModel>(),
                MajorIndexListingViewModel.LoadMajorIndexViewModel(services.GetRequiredService<IMajorIndexService>()));
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {   // pass in the renavigators that allow us to renaviagte to a certain page
            return new LoginViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {   // this takes two renavigators that are the same in case we want to change
            // where the registration page goes after we register
            return new RegisterViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
        }
    }
}
