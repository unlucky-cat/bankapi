using BankAPI.DefaultImplementations;
using BankAPI.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSUnitTests
{
    [TestClass]
    public class Working_wint_client_funds
    {
        [TestMethod]
        public void Jack_is_opening_an_account()
        {
            //Arrange
            var customers = new GenericInMemoryRepository<Customer>();
            var accounts = new GenericInMemoryRepository<Account>();
            var journal = new GenericInMemoryRepository<FinancialTransaction>();

            var bank = new Bank("EUR", 1000000m, customers, accounts, journal);

            // Act
            var acc = bank.OpenClientMoneyAccount(new Customer("Andrey"));
            var balance = bank.GetAccountBalance(acc).Amount;

            // Assert
            Assert.IsTrue(1000000m == bank.Equity.Amount, "Bank equity is equal to seed capital for a newly created bank");
            Assert.IsTrue(0m == balance, "Initial balance of a new account equals to zero");


            bank.DepositNonCash(acc, new Money(1000m, "EUR"));
            var balanceAfterDeposit = bank.GetAccountBalance(acc).Amount;

            Assert.IsTrue(-1000m == balanceAfterDeposit, "After depositing 1000 there should be 1000 in the account");

            // var otherAcc = bank.OpenClientMoneyAccount(new Customer("Mary"));
            // bank.TransferMoney(acc, otherAcc, 400);

            // var balanceAfterTransfer = bank.GetAccountBalance(acc).Amount;
            // Assert.True(600m == balanceAfterTransfer, "After depositing 1000 and transfering 400 there should be 600 in the account");
        }
    }
}