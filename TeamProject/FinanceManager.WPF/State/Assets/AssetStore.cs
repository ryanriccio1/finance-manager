using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services;
using FinanceManager.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.State.Assets
{
    public class AssetStore
    {
        private readonly IAccountStore _accountStore;
        private readonly IStockPriceService _stockPriceService;

        public double AccountBalance => _accountStore.CurrentAccount?.Balance ?? 0;
        private double _portfolioValue = 0;
        public double PortfolioValue { get; set; }
        // calculate the assesttransactions by either getting them from the current account or creating an empty list of it
        public IEnumerable<AssetTransaction> AssetTransactions => _accountStore.CurrentAccount?.AssetTransactions ?? new List<AssetTransaction>();
        public event Action StateChanged;

        public AssetStore(IAccountStore accountStore, IStockPriceService stockPriceService)
        {
            _accountStore = accountStore;
            _stockPriceService = stockPriceService;
            OnStateChanged();
            // when the account changes, raise our event as well
            _accountStore.StateChanged += OnStateChanged;
        }

        // create an even handler that will invoke a state changed to anyone subscribed to us
        private async void OnStateChanged()
        {
            await GetPortfolioValue();
            StateChanged?.Invoke();
        }

        private async Task GetPortfolioValue()
        {
            _portfolioValue = 0;
            foreach (var asset in AssetTransactions)
            {
                if (asset.IsPurchase)
                {
                    _portfolioValue += asset.Shares * await _stockPriceService.GetPrice(asset.Asset.Symbol);
                }
                else if (!asset.IsPurchase)
                {
                    _portfolioValue -= asset.Shares * await _stockPriceService.GetPrice(asset.Asset.Symbol);
                }
            }
            PortfolioValue = _portfolioValue;
        }
    }
}
