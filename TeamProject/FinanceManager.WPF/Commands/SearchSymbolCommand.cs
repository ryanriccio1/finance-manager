using FinanceManager.Domain.Exceptions;
using FinanceManager.Domain.Services;
using FinanceManager.WPF.ViewModels;
using SimpleTrader.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FinanceManager.WPF.Commands
{
    public class SearchSymbolCommand : AsyncCommandBase
    {
        private readonly ISearchSymbolViewModel _viewModel;
        private readonly IStockPriceService _stockPriceService;


        public SearchSymbolCommand(ISearchSymbolViewModel viewModel, IStockPriceService stockPriceService)
        {
            _viewModel = viewModel;
            _stockPriceService = stockPriceService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                // get the price of the stock and update it on the view model passed in
                double stockPrice = await _stockPriceService.GetPrice(_viewModel.Symbol);
                _viewModel.SearchResultSymbol = _viewModel.Symbol.ToUpper();
                _viewModel.StockPrice = stockPrice;
            }
            // show the error messages
            catch (InvalidSymbolException)
            {
                _viewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (Exception) 
            {
                _viewModel.ErrorMessage = "Failed to get symbol information.";
            }
        }
    }
}
