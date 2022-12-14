using FinanceManager.Domain.Services;
using FinanceManager.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FinanceManager.WPF.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly IStockPriceService _stockPriceService;

        public double AccountBalance => _assetStore.AccountBalance;

        private double _portfolioValue = 0;
        public double PortfolioValue
        {
            get
            {
                return _portfolioValue;
            }
            set
            {
                _portfolioValue = value;
                OnPropertyChanged(nameof(PortfolioValue));
            }
        }

        public AssetListingViewModel AssetListingViewModel { get; }        

        public AssetSummaryViewModel(AssetStore assetStore, IStockPriceService stockPriceService)
        {
            _assetStore = assetStore;
            _stockPriceService = stockPriceService;
            AssetListingViewModel = new AssetListingViewModel(assetStore, assets => assets.Take(3), _stockPriceService);

            _assetStore.StateChanged += AssetStore_StateChanged;
        }

        private void AssetStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
            PortfolioValue = _assetStore.PortfolioValue;
        }
    }
}
