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
    public class AssetListingViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> _filterAssets;
        private readonly ObservableCollection<AssetViewModel> _assets;
        private readonly IStockPriceService _stockPriceService;

        public IEnumerable<AssetViewModel> Assets => _assets;

        public AssetListingViewModel(AssetStore assetStore, IStockPriceService stockPriceService) : this(assetStore, assets => assets, stockPriceService) { }

        public AssetListingViewModel(AssetStore assetStore, Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> filterAssets, IStockPriceService stockPriceService)
        {
            _assetStore = assetStore;
            _filterAssets = filterAssets;
            _stockPriceService = stockPriceService;
            _assets = new ObservableCollection<AssetViewModel>();

            _assetStore.StateChanged += AssetStore_StateChanged;

            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = _assetStore.AssetTransactions
                .GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares), _stockPriceService))
                .Where(a => a.Shares > 0)
                .OrderByDescending(a => a.Shares);

            assetViewModels = _filterAssets(assetViewModels);

            _assets.Clear();
            foreach (AssetViewModel viewModel in assetViewModels)
            {
                _assets.Add(viewModel);
            }
        }

        private void AssetStore_StateChanged()
        {
            ResetAssets();
        }
    }
}
