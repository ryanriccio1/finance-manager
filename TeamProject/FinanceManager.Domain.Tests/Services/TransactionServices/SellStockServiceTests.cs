using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services.TransactionServices;
using FinanceManager.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Exceptions;

namespace FinanceManager.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class SellStockServiceTests
    {
        private SellStockService _sellStockService;

        private Mock<IStockPriceService> _mockStockPriceService;
        private Mock<IDataService<Account>> _mockAccountService;

        [SetUp]
        public void SetUp()
        {
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountService = new Mock<IDataService<Account>>();

            _sellStockService = new SellStockService(_mockStockPriceService.Object, _mockAccountService.Object);
        }

        [Test]
        public void SellStock_WithInsufficientShares_ThrowsInsufficientSharesException()
        {
            string expectedSymbol = "T";
            int expectedAccountShares = 0;
            int expectedRequiredShares = 10;
            Account seller = CreateAccount(expectedSymbol, expectedAccountShares);

            // when we try to sell the stock, make sure that we throw exception
            InsufficientSharesException exception = Assert.ThrowsAsync<InsufficientSharesException>(
                () => _sellStockService.SellStock(seller, expectedSymbol, expectedRequiredShares));
            
            // make sure that that data in our exception was set properly
            string actualSymbol = exception.Symbol;
            double actualAccountBalance = exception.AccountShares;
            double actualRequiredBalance = exception.RequiredShares;

            Assert.AreEqual(expectedSymbol, actualSymbol);
            Assert.AreEqual(expectedAccountShares, actualAccountBalance);
            Assert.AreEqual(expectedRequiredShares, actualRequiredBalance);
        }

        [Test]
        public void SellStock_WithInvalidSymbol_ThrowsInvalidSymbolExceptionForSymbol()
        {
            string expectedInvalidSymbol = "bad_symbol";
            Account seller = CreateAccount(expectedInvalidSymbol, 10);

            // when we get a price, throw an exception
            _mockStockPriceService.Setup(s => s.GetPrice(expectedInvalidSymbol)).ThrowsAsync(new InvalidSymbolException(expectedInvalidSymbol));

            // make sure we throw an exception
            InvalidSymbolException exception = Assert.ThrowsAsync<InvalidSymbolException>(() => _sellStockService.SellStock(seller, expectedInvalidSymbol, 5));
            
            // make sure the props in the exception were set properly
            string actualInvalidSymbol = exception.Symbol;
            Assert.AreEqual(expectedInvalidSymbol, actualInvalidSymbol);
        }

        [Test]
        public void SellStock_WithGetPriceFailure_ThrowsException()
        {
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            // make the price service fail for a general reason
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ThrowsAsync(new Exception());

            // make sure the error makes it to our sell stock service
            Assert.ThrowsAsync<Exception>(() => _sellStockService.SellStock(seller, It.IsAny<string>(), 5));
        }

        [Test]
        public void SellStock_WithAccountUpdateFailure_ThrowsException()
        {
            Account seller = CreateAccount(It.IsAny<string>(), 10);

            // when we ask to update an account, throw an exception for some reason (this might come from our db)
            _mockAccountService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Account>())).ThrowsAsync(new Exception());

            // make sure the exception makes it through our sell stock service
            Assert.ThrowsAsync<Exception>(() => _sellStockService.SellStock(seller, It.IsAny<string>(), 5));
        }

        [Test]
        public async Task SellStock_WithSuccessfulSell_ReturnsAccountWithNewTransaction()
        {
            int expectedTransactionCount = 2;
            Account seller = CreateAccount(It.IsAny<string>(), 10);

            seller = await _sellStockService.SellStock(seller, It.IsAny<string>(), 5);
            int actualTransactionCount = seller.AssetTransactions.Count;

            // make sure that selling a stock will increase our amount of asset transactions
            Assert.AreEqual(expectedTransactionCount, actualTransactionCount);
        }

        [Test]
        public async Task SellStock_WithSuccessfulSell_ReturnsAccountWithNewBalance()
        {
            double expectedBalance = 100;
            Account seller = CreateAccount(It.IsAny<string>(), 10);

            // when we ask for a price, give us a valid price
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(50);

            seller = await _sellStockService.SellStock(seller, It.IsAny<string>(), 2);
            double actualBalance = seller.Balance;

            // make sure our balance changed properly
            Assert.AreEqual(expectedBalance, actualBalance);
        }

        private Account CreateAccount(string symbol, int shares)
        {
            // create generic account
            return new Account()
            {
                AssetTransactions = new List<AssetTransaction>()
                {
                    new AssetTransaction()
                    {
                        Asset = new Asset()
                        {
                            Symbol = symbol
                        },
                        IsPurchase = true,
                        Shares = shares
                    }
                }
            };
        }
    }
}
