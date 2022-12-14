using FinanceManager.Domain.Services;
using FinanceManager.Domain.Services.TransactionServices;
using FinanceManager.WPF.Commands;
using FinanceManager.WPF.State.Accounts;
using FinanceManager.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.WPF.ViewModels
{
    public class SellViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        private readonly AssetStore _assetStore;
        public AssetListingViewModel AssetListingViewModel { get; set; }
        
        // generic prop change that updates view model
        private AssetViewModel _selectedAsset;
        public AssetViewModel SelectedAsset
        {
            get
            {
                return _selectedAsset;
            }
            set
            {
                _selectedAsset = value;
                OnPropertyChanged(nameof(SelectedAsset));
            }
        }

        // the symbol is calculated
        private string _symbol;
        public string Symbol => SelectedAsset?.Symbol;

        private string _searchResultSymbol = string.Empty;
        public string SearchResultSymbol
        {
            get
            {
                return _searchResultSymbol;
            }
            set
            {
                _searchResultSymbol = value;
                OnPropertyChanged(nameof(SearchResultSymbol));
            }
        }

        private double _stockPrice;
        public double StockPrice
        {
            get
            {
                return _stockPrice;
            }
            set
            {
                _stockPrice = value;
                OnPropertyChanged(nameof(StockPrice));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private int _sharesToSell;
        public int SharesToSell
        {
            get
            {
                return _sharesToSell;
            }
            set
            {
                _sharesToSell = value;
                NewPortfolioValue = _assetStore.PortfolioValue - TotalPrice;
                OnPropertyChanged(nameof(SharesToSell));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private double _newPortfolioValue;
        public double NewPortfolioValue
        {
            get
            {
                return _newPortfolioValue;
            }
            set
            {
                _newPortfolioValue = value;
                OnPropertyChanged(nameof(NewPortfolioValue));
            }
        }

        public double TotalPrice => SharesToSell * StockPrice;
        
        // store data about the error message in a prop change and
        // have an event that we can subscribe to
        public MessageViewModel ErrorMessageViewModel { get; }
        
        // store this in a property relative to this
        // class so classes using this view model 
        // do not need to know about the MessageViewModel
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public MessageViewModel StatusMessageViewModel { get; }
        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;
        }

        public ICommand SearchSymbolCommand { get; }
        public ICommand SellStockCommand { get; }

        public SellViewModel(AssetStore assetStore, 
            IStockPriceService stockPriceService,
            IAccountStore accountStore,
            ISellStockService sellStockService)
        {
            _assetStore = assetStore;
            NewPortfolioValue = _assetStore.PortfolioValue + TotalPrice;

            // load all of the view models that come with this
            AssetListingViewModel = new AssetListingViewModel(_assetStore, stockPriceService);

            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            SellStockCommand = new SellStockCommand(this, sellStockService, accountStore);

            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();
        }
    }
}
