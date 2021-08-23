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

            var bank = new Bank("EUR", 1000000f, customers, accounts, journal);

            // Act
            var acc = bank.OpenNonCashAccount(new Customer("Andrey"));
            
            // Assert
            //Assert.True(0f == acc.Balance.Amount, "Initial balance for a new account equals to zero");
            Assert.True(1000000f == bank.Equity.Amount, "Bank equity is equal to seed capital for a newly created bank");
        }
    }
}
