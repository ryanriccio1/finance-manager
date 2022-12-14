using FinanceManager.Domain.Exceptions;
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services;
using FinanceManager.Domain.Services.AuthenticationServices;
using Microsoft.AspNet.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private Mock<IAccountService> _mockAccountService;
        private AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);
        }


        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {
            string expectedUsername = "testuser";
            string password = "testpassword";

            // the account service should return a new account with the correct user when asked for the user
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            
            // the password hasher should be able to verify the hash if it is the same as the test password
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);

            // perform the login
            Account account = await _authenticationService.Login(expectedUsername, password);

            // get the username we returned and make sure that it is equal to the
            // user we logged in with
            string actualUsername = account.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithIncorrectPasswordForExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            string expectedUsername = "testuser";
            string password = "testpassword";

            // the account service should return a new account with the correct user when asked for the user
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            
            // the password hasher should be able to correctly identify that the password is incorrect
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            // make sure that we handle the exception properly when we login with an incorrect password
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() => _authenticationService.Login(expectedUsername, password));

            // make sure we were on the correct username the whole time
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            string expectedUsername = "testuser";
            string password = "testpassword";

            // return incorrect password
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            // make sure we throw an exception when we do not find the user
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUsername, password));

            // make sure we were on the correct user the whole time
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {
            string password = "testpassword";
            string confirmPassword = "confirmtestpassword";

            // we expect the passwords to not match
            RegistrationResult expected = RegistrationResult.PasswordsDoNotMatch;

            // try to register with incorrect password
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);

            // make sure the state was return that the passwords do not match
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnsEmailAlreadyExists()
        {
            string email = "test@gmail.com";

            // return a non null account when we ask for an email
            _mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new Account());
            
            // we expect the email to already exist
            RegistrationResult expected = RegistrationResult.EmailAlreadyExits;

            // try to register with an existing email
            RegistrationResult actual = await _authenticationService.Register(email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            // make sure the state was returned that the email already exists
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnsUsernameAlreadyExists()
        {
            // Arrange
            string username = "testuser";

            // return a non null account when we ask for it by username
            _mockAccountService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new Account());

            // we expect the account to already exist
            RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;

            // try to register with an existing username
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), username, It.IsAny<string>(), It.IsAny<string>());

            // make sure the state was returned that the username already exists
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPasswords_ReturnsSuccess()
        {
            // check to make sure that a code path returns successful
            RegistrationResult expected = RegistrationResult.Success;

            // try to register
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            // make sure we are successful
            Assert.AreEqual(expected, actual);
        }
    }
}
