using FinanceManager.Domain.Models;
using FinanceManager.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services.TransactionServices
{
    public interface ISellStockService
    {
        /// <summary>
        /// Sell a stock for an account.
        /// </summary>
        /// <param name="seller">The account of the seller.</param>
        /// <param name="symbol">The symbol sold.</param>
        /// <param name="shares">The amount of shares to sell.</param>
        /// <returns>The updated account.</returns>
        /// <exception cref="InvalidSymbolException">Thrown if the purchased symbol is invalid.</exception>
        /// <exception cref="InsufficientSharesException">Thrown if the user does not have enough shares.</exception>
        /// <exception cref="Exception">Thrown if the transaction fails.</exception>
        Task<Account> SellStock(Account seller, string symbol, int shares);
    }
}
