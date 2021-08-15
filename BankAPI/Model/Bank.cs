using System.Collections.Generic;
using BankAPI.Interfaces;

namespace BankAPI.Model {

    public class Bank {

        private string defaultCurrency;
        private ICustomerRepository customers;
        private List<Account> accounts;
        
        public Bank(string DefaultCurrency, ICustomerRepository customerRepository) {
            
            this.defaultCurrency = DefaultCurrency;
            this.customers = customerRepository;
            this.accounts = new List<Account>();
        }
        public Account OpenAccount(Customer customer) {

            customers.Add(customer);
            var acc = this.CreateAccount(customer, new Money(0f, this.defaultCurrency));

            accounts.Add(acc);

            return acc;
        }

        private Account CreateAccount(Customer customer, Money money) {
            return new Account(customer, money);
        }
    }
}