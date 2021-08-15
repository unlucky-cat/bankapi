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
            
            var customers = new InMemoryCustomerRepository();
            var bank = new Bank("EUR", customers);
            var acc = bank.OpenAccount(new Customer());
            

            Assert.Equal(0f, acc.Balance.Amount);
        }
    }
}
