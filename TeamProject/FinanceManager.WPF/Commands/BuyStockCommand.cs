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
using System.Windows;
using System.Windows.Input;

namespace FinanceManager.WPF.Commands
{
    public class BuyStockCommand : AsyncCommandBase
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accountStore = accountStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _buyViewModel.ErrorMessage = string.Empty;
            _buyViewModel.StatusMessage = string.Empty;

            try
            {   // get data from the view model and send it to the buystockservice 
                // and then update our account
                string symbol = _buyViewModel.Symbol.ToUpper();
                int shares = _buyViewModel.SharesToBuy;
                Account account = await _buyStockService.BuyStock(_accountStore.CurrentAccount, symbol, shares);
                _accountStore.CurrentAccount = account;
                _buyViewModel.StatusMessage = $"Successfully purchased {shares} shares of {symbol}.";
            }
            // handle exceptions
            catch (InsufficientFundsException)
            {
                _buyViewModel.ErrorMessage = "Account has insufficient funds. Please transfer more money into your account.";
            }
            catch (InvalidSymbolException)
            {
                _buyViewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (Exception)
            {
                _buyViewModel.ErrorMessage = "Transaction failed.";
            }
        }
    }
}
