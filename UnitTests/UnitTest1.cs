using System;
using BankAPI.Model;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var bank = new Bank("Eur");
            var acc = bank.OpenAccount(new Person());
            

            Assert.Equal(0f, acc.Balance.Amount);
        }
    }
}
