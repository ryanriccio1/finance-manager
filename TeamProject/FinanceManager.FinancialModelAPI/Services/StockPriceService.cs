using FinanceManager.Domain.Exceptions;
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services;
using FinanceManager.FinancialModelAPI.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.FinancialModelAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly FinancialModelAPIHttpClient _client;

        public StockPriceService(FinancialModelAPIHttpClient client)
        {
            _client = client;
        }

        public async Task<double> GetPrice(string symbol)
        {
            string uri = "stock/real-time-price/" + symbol;

            // deserialize the stock data into an object
            StockPriceResult stockPriceResult = await _client.GetAsync<StockPriceResult>(uri);
                
            if (stockPriceResult.Price == 0)
            {
                throw new InvalidSymbolException(symbol);
            }

            // return the price of the result
            return stockPriceResult.Price;
        }

        public async Task<double> GetDayChange(string symbol)
        {
            StockPercentResult stockPercentResult = await GetPercentChange(symbol);
            return stockPercentResult._1D;
        }

        public async Task<double> GetWeekChange(string symbol)
        {
            StockPercentResult stockPercentResult = await GetPercentChange(symbol);
            return stockPercentResult._5D;
        }

        public async Task<double> GetMonthChange(string symbol)
        {
            StockPercentResult stockPercentResult = await GetPercentChange(symbol);
            return stockPercentResult._1M;
        }

        public async Task<double> GetYearChange(string symbol)
        {
            StockPercentResult stockPercentResult = await GetPercentChange(symbol);
            return stockPercentResult._1Y;
        }

        private async Task<StockPercentResult> GetPercentChange(string symbol)
        {
            string uri = "stock-price-change/" + symbol;
            List<StockPercentResult> stockPercentResult = await _client.GetAsync<List<StockPercentResult>>(uri);
            return stockPercentResult[0];
        }
    }
}
