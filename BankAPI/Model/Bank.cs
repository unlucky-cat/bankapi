using System.Collections.Generic;

namespace BankAPI.Model {

    public class Bank {

        private string defaultCurrency;
        private List<Customer> customers;
        private List<Account> accounts;
        
        public Bank(string DefaultCurrency) {
            
            this.defaultCurrency = DefaultCurrency;
            this.customers = new List<Customer>();
            this.accounts = new List<Account>();
        }
        public Account OpenAccount(Customer person) {

            var acc = this.CreateAccount(person, new Money(0f, this.defaultCurrency));

            customers.Add(person);
            accounts.Add(acc);

            return acc;
        }

        private Account CreateAccount(Customer customer, Money money) {
            return new Account(customer, money);
        }
    }
}