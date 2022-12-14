using FinanceManager.Domain.Models;
using FinanceManager.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services.TransactionServices
{
    public class BuyStockService : IBuyStockService
    {
        // service to get stock price
        private readonly IStockPriceService _stockPriceService;
        
        // service to perform account actions on the database
        private readonly IDataService<Account> _accountService;

        // instantiate members
        public BuyStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        public async Task<Account> BuyStock(Account buyer, string symbol, int shares)
        {
            // get the stock price and total transaction price
            double stockPrice = await _stockPriceService.GetPrice(symbol);
            double transactionPrice = stockPrice * shares;

            // make sure we have enoug money in the account
            if (transactionPrice > buyer.Balance)
            {
                throw new InsufficientFundsException(buyer.Balance, transactionPrice);
            }

            // store a new transaction
            AssetTransaction transaction = new AssetTransaction()
            {
                Account = buyer,
                Asset = new Asset()
                {
                    PricePerShare = stockPrice,
                    Symbol = symbol
                },
                DateProcessed = DateTime.Now,
                Shares = shares,
                IsPurchase = true
            };

            // add to the buyer, update the buyer on the database
            buyer.AssetTransactions.Add(transaction);
            buyer.Balance -= transactionPrice;

            await _accountService.Update(buyer.Id, buyer);
            
            // return updated buyer
            return buyer;
        }

    }
}
