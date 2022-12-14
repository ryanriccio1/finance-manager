using FinanceManager.Domain.Exceptions;
using FinanceManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services.TransactionServices
{
    public class SellStockService : ISellStockService
    {
        // service to get the price of the stock
        private readonly IStockPriceService _stockPriceService;

        // service to get the account data
        // we can use IDataService instead of IAccountService since we are getting by Id
        private readonly IDataService<Account> _accountService;

        // instantiate members
        public SellStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        public async Task<Account> SellStock(Account seller, string symbol, int shares)
        {
            // validate seller has sufficient shares
            int accountShares = GetAcountSharesForSymbol(seller, symbol);
            if (accountShares < shares) 
            {
                throw new InsufficientSharesException(symbol, accountShares, shares);
            }

            // get the price of the stock
            double stockPrice = await _stockPriceService.GetPrice(symbol);

            // add an AssetTransaction for the sale to the users account
            seller.AssetTransactions.Add(new AssetTransaction()
            {
                Account = seller,
                Asset = new Asset()
                {
                    PricePerShare = stockPrice,
                    Symbol = symbol
                },
                DateProcessed = DateTime.Now,
                IsPurchase = false,
                Shares = shares
            });

            // update the account
            seller.Balance += stockPrice * shares;
            await _accountService.Update(seller.Id, seller);

            // return updated account
            return seller;
        }

        private int GetAcountSharesForSymbol(Account seller, string symbol)
        {
            // return a list of transactions where the symbol of the asset is the symbol we are searching for
            IEnumerable<AssetTransaction> accountTransactionsForSymbol = seller.AssetTransactions.Where(a => a.Asset.Symbol == symbol);

            // return the summation of all of the searched symbol, with positive number of shares for purchases
            // and negative values of shares for previous sales
            return accountTransactionsForSymbol.Sum(a => a.IsPurchase ? a.Shares : -a.Shares);
        }
    }
}
