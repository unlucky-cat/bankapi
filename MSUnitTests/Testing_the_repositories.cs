using System.Linq;
using BankAPI.DefaultImplementations;
using BankAPI.Interfaces;
using BankAPI.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSUnitTests
{
    [TestClass]
    public class Testing_the_repositories
    {
        [TestMethod]
        public void Jack_is_opening_an_account()
        {
            //Arrange
            IGenericRepository<Customer> customers = new GenericInMemoryRepository<Customer>();
            IGenericRepository<Account> accounts = new GenericInMemoryRepository<Account>();
            var journal = new GenericInMemoryRepository<FinancialTransaction>();

            var bank = new Bank("EUR", 1000000m, customers, accounts, journal);
            var cust = new Customer("Andrey");

            // Act
            var acc = bank.OpenClientMoneyAccount(cust);

            Assert.IsTrue(customers.GetRecords().Where(c => c.Name.Equals(cust.Name)).Count() == 1, "fff");
        }
    }
}
