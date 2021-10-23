using BankAPI.DefaultImplementations;
using BankAPI.Interfaces;
using BankAPI.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSUnitTests
{
    [TestClass]
    public class Working_with_private_accounts
    {
        private static Bank bank;

        [ClassInitialize()]
        public static void Init(TestContext testContext)
        {
            bank = Bank.CreateInMemoryBank("EUR", 1000000m);
        }

        [TestMethod]
        public void Creating_a_bank_instance()
        {
            var customers = new GenericInMemoryRepository<Customer>();
            var accounts = new GenericInMemoryRepository<Account>();
            var journal = new GenericInMemoryRepository<FinancialTransaction>();

            var _bank = new Bank("EUR", 1000000m, customers, accounts, journal);

            Assert.IsTrue(1000000m == _bank.Equity.Amount, "Bank equity is equal to the seed capital for a newly created bank");
        }

        [TestMethod]
        public void Opening_an_empty_client_funds_account()
        {
            var acc = bank.OpenClientAccount(new Customer("Andrey"));
            var balance = bank.GetAccountBalance(acc).Amount;

            // Assert
            
            Assert.IsTrue(0m == balance, "Initial balance of a new account equals to zero");


            bank.DepositNonCash(acc, new Money(1000m, "EUR"));
            var balanceAfterDeposit = bank.GetAccountBalance(acc).Amount;

            Assert.IsTrue(1000m == balanceAfterDeposit, "After depositing 1000 there should be 1000 on the account");
        }

        [TestMethod]
        public void Opening_a_client_funds_account_and_adding_some_money_to_it()
        {
            var acc = bank.OpenClientAccount(new Customer("Andrey"));
            bank.DepositNonCash(acc, new Money(1000m, "EUR"));

            var balance = bank.GetAccountBalance(acc).Amount;

            Assert.IsTrue(1000m == balance, "After depositing 1000 there should be 1000 on the account");
        }
    }
}