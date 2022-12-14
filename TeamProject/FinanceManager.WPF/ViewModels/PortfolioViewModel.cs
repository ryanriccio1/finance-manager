using FinanceManager.Domain.Services;
using FinanceManager.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.ViewModels
{
    public class PortfolioViewModel : ViewModelBase
    {
        public AssetListingViewModel AssetListingViewModel { get; }

        public PortfolioViewModel(AssetStore assetStore, IStockPriceService stockPriceService)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore, stockPriceService);
        }
    }
}
