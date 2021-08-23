using System;
using BankAPI.Model;
using BankAPI.Interfaces;
using BankAPI.DefaultImplementations;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            var customers = new InMemoryCustomerRepository();
            var accounts = new InMemoryAccountRepository();
            var journal = new InMemoryJournalRepository();

            var bank = new Bank("EUR", 1000000m, customers, accounts, journal);

            // Act
            var acc = bank.OpenNonCashAccount(new Customer("Andrey"));
            var balance = bank.GetAccountBalance(acc).Amount;
            
            // Assert
            Assert.True(1000000m == bank.Equity.Amount, "Bank equity is equal to seed capital for a newly created bank");
            Assert.True(0m == balance, "Initial balance of a new account equals to zero");
        }
    }
}
