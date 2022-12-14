using FinanceManager.Domain.Models;
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
    public class BuyViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        private readonly AssetStore _assetStore;

        private string _symbol;
        public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value;
                OnPropertyChanged(nameof(Symbol));
            }
        }

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

        private int _sharesToBuy;
        public int SharesToBuy
        {
            get
            {
                return _sharesToBuy;
            }
            set
            {
                _sharesToBuy = value;
                NewPortfolioValue = _assetStore.PortfolioValue + TotalPrice;
                OnPropertyChanged(nameof(SharesToBuy));
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

        public double TotalPrice
        {
            get
            {
                return SharesToBuy * StockPrice;
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public MessageViewModel StatusMessageViewModel { get; }
        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;
        }

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        public BuyViewModel(IStockPriceService stockPriceService, IBuyStockService buyStockService, IAccountStore accountStore, AssetStore assetStore)
        {
            _assetStore = assetStore;
            NewPortfolioValue = _assetStore.PortfolioValue + TotalPrice;

            _assetStore.StateChanged += AssetStore_StateChanged;
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();

            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService, accountStore);
        }

        private void AssetStore_StateChanged()
        {
            NewPortfolioValue = _assetStore.PortfolioValue - TotalPrice;
        }
    }
}
