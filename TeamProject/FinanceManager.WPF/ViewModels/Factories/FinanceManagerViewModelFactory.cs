using FinanceManager.FinancialModelAPI.Services;
using FinanceManager.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.ViewModels.Factories
{
    public class FinanceManagerViewModelFactory : IFinanceManagerViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<PortfolioViewModel> _createPortfolioViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<BuyViewModel> _createBuyViewModel;
        private readonly CreateViewModel<SellViewModel> _createSellViewModel;

        // store the lamda expressions to create view models
        public FinanceManagerViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel,
                                              CreateViewModel<PortfolioViewModel> createPortfolioViewModel,
                                              CreateViewModel<LoginViewModel> createLoginViewModel,
                                              CreateViewModel<BuyViewModel> createBuyViewModel,
                                              CreateViewModel<SellViewModel> createSellViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createPortfolioViewModel = createPortfolioViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createBuyViewModel = createBuyViewModel;
            _createSellViewModel = createSellViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {   // create the view models
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.Portfolio:
                    return _createPortfolioViewModel();
                case ViewType.Buy:
                    return _createBuyViewModel();
                case ViewType.Login:
                    return _createLoginViewModel();
                case ViewType.Sell:
                    return _createSellViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}
