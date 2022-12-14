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
    public class BuyStockServiceTests
    {
        private Mock<IStockPriceService> _mockStockPriceService;
        private Mock<IDataService<Account>> _mockAccountService;
        private BuyStockService _buyStockService;

        [SetUp]
        public void SetUp()
        {
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountService = new Mock<IDataService<Account>>();

            _buyStockService = new BuyStockService(_mockStockPriceService.Object, _mockAccountService.Object);
        }

        [Test]
        public void BuyStock_WithInvalidSymbol_ThrowsInvalidSymbolExceptionForSymbol()
        {
            string expectedInvalidSymbol = "bad_symbol";

            // the stock price service should throw an exception with a known bad_symbol
            _mockStockPriceService.Setup(s => s.GetPrice(expectedInvalidSymbol)).ThrowsAsync(new InvalidSymbolException(expectedInvalidSymbol));

            // make sure that we throw this exception
            InvalidSymbolException excpetion = Assert.ThrowsAsync<InvalidSymbolException>(
                () => _buyStockService.BuyStock(It.IsAny<Account>(), expectedInvalidSymbol, It.IsAny<int>()));
            
            // make sure that the Symbol being used is correctly set
            string actualInvalidSymbol = excpetion.Symbol;
            Assert.AreEqual(expectedInvalidSymbol, actualInvalidSymbol);
        }

        [Test]
        public void BuyStock_WithGetPriceFailure_ThrowsException()
        {
            // when GetPrice fails for a non specific reason, make sure that Buy stock throws the exception
            // as well
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(
                () => _buyStockService.BuyStock(It.IsAny<Account>(), It.IsAny<string>(), It.IsAny<int>()));
        }

        [Test]
        public void BuyStock_WithInsufficientFunds_ThrowsInsufficientFundsExceptionForBalances()
        {
            double expectedAccountBalance = 0;
            double expectedRequiredBalance = 100;
            Account buyer = CreateAccount(expectedAccountBalance);

            // when we try to get a price return  the higher price (the one that is too heigh)
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(expectedRequiredBalance);

            // make sure that we throw an exception
            InsufficientFundsException exception = Assert.ThrowsAsync<InsufficientFundsException>(
                () => _buyStockService.BuyStock(buyer, It.IsAny<string>(), 1));
            
            // make sure that the values in the exception were set properly
            double actualAccountBalance = exception.AccountBalance;
            double actualRequiredBalance = exception.RequiredBalance;

            Assert.AreEqual(expectedAccountBalance, actualAccountBalance);
            Assert.AreEqual(expectedRequiredBalance, actualRequiredBalance);
        }

        [Test]
        public void BuyStock_WithAccountUpdateFailure_ThrowsException()
        {
            Account buyer = CreateAccount(1000);

            // when we try to get the price, return a valid price
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(100);
            
            // when we try to update the database, throw an exception
            _mockAccountService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Account>())).Throws(new Exception());

            // make sure this exception makes it to the BuyStock method
            Assert.ThrowsAsync<Exception>(() => _buyStockService.BuyStock(buyer, It.IsAny<string>(), 1));
        }

        [Test]
        public async Task BuyStock_WithSuccessfulPurchase_ReturnsAccountWithNewTransaction()
        {
            int expectedTransactionCount = 1;
            Account buyer = CreateAccount(1000);

            // when we ask for a price, give us a valid price
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(100);

            buyer = await _buyStockService.BuyStock(buyer, It.IsAny<string>(), 1);
            int actualTransactionCount = buyer.AssetTransactions.Count();

            // check to make sure we increased the amount of asset transactions
            Assert.AreEqual(expectedTransactionCount, actualTransactionCount);
        }

        [Test]
        public async Task BuyStock_WithSuccessfulPurchase_ReturnsAccountWithNewBalance()
        {
            double expectedBalance = 0;
            Account buyer = CreateAccount(100);

            // when we ask for a price, give us a valid price
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(50);

            buyer = await _buyStockService.BuyStock(buyer, It.IsAny<string>(), 2);
            double actualBalance = buyer.Balance;

            // check to make sure our balance is what we expected
            Assert.AreEqual(expectedBalance, actualBalance);
        }

        private Account CreateAccount(double balance)
        {
            // create default account with no assets
            return new Account()
            {
                Balance = balance,
                AssetTransactions = new List<AssetTransaction>()
            };
        }
    }
}
