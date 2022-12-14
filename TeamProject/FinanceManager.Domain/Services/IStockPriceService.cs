using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    /// <summary>
    /// Service to get info about a stock.
    /// </summary>
    public interface IStockPriceService
    {
        /// <summary>
        /// Get the stock price.
        /// </summary>
        /// <param name="symbol">Symbol to get the price of.</param>
        /// <returns>The price of the symbol.</returns>
        Task<double> GetPrice(string symbol);

        Task<double> GetDayChange(string symbol);
        Task<double> GetWeekChange(string symbol);
        Task<double> GetMonthChange(string symbol);
        Task<double> GetYearChange(string symbol);
    }
}
