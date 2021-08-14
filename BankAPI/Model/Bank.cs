using System.Collections.Generic;

namespace BankAPI.Model {

    public class Bank {

        private string defaultCurrency;
        private List<Person> persons;
        private List<Account> accounts;
        public Bank(string DefaultCurrency) {
            
            this.defaultCurrency = DefaultCurrency;
            this.persons = new List<Person>();
            this.accounts = new List<Account>();
        }
        public Account OpenAccount(Person person) {

            var acc = this.CreateAccount(person, new Money(0f, this.defaultCurrency));

            persons.Add(person);
            accounts.Add(acc);

            return acc;
        }

        private Account CreateAccount(Person person, Money money) {
            return new Account(person, money);
        }
    }
}