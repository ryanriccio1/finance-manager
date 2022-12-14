using FinanceManager.Domain.Exceptions;
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services.TransactionServices;
using FinanceManager.WPF.State.Accounts;
using FinanceManager.WPF.ViewModels;
using SimpleTrader.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        private readonly SellViewModel _viewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;

        public SellStockCommand(SellViewModel viewModel, ISellStockService sellStockService, IAccountStore accountStore)
        {
            _viewModel = viewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;
        }

        public async override Task ExecuteAsync(object parameter)
        {
            // clear all the messages before we start to add more
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.StatusMessage = string.Empty;

            try
            {
                string symbol = _viewModel.Symbol.ToUpper();
                int shares = _viewModel.SharesToSell;
                
                // get a new account after we sell a stock
                Account account = await _sellStockService.SellStock(_accountStore.CurrentAccount, symbol, shares);
                
                // update the state of the current account for the entire app
                _accountStore.CurrentAccount = account;

                // display stuff to the user
                _viewModel.SearchResultSymbol = string.Empty;
                _viewModel.StatusMessage = $"Successfully sold {shares} shares of {symbol}.";
            }
            // handle all the exceptions
            catch (InsufficientSharesException ex)
            {
                _viewModel.ErrorMessage = $"Account has insufficient shares. You only have {ex.AccountShares} shares.";
            }
            catch (InvalidSymbolException)
            {
                _viewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Transaction failed.";
            }
        }
    }
}
