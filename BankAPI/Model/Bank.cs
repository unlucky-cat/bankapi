using System.Collections.Generic;
using BankAPI.Interfaces;
using System;
using System.Linq;

namespace BankAPI.Model {

    public class Bank {

        private string defaultCurrency;
        private readonly Account ClientMoneyAccount;
        private Money equity;

        /// <summary> Investment of assets in a business by the owner or owners is called <c>capital</c>. </summary>
        public Money Equity {
            get { return equity; }
        }
        private IGenericRepository<Customer> customers;
        private IGenericRepository<Account> accounts;
        private IGenericRepository<FinancialTransaction> journal;
        
        public Bank(
            string defaultCurrency, 
            decimal seedCapital,
            IGenericRepository<Customer> customerRepository, 
            IGenericRepository<Account> accountRepository,
            IGenericRepository<FinancialTransaction> journalRepository
        ) {
            
            var initialCapital = new Money(seedCapital, defaultCurrency);
            this.defaultCurrency = defaultCurrency;
            this.equity = initialCapital;
            this.customers = customerRepository;
            this.accounts = accountRepository;
            this.journal = journalRepository;

            var owner = new Customer("The Owner");
            this.customers.Add(owner);

            var bank = new Customer("The Bank");
            this.customers.Add(bank);

            var ownerAccount = new Account(owner, (ushort) AccountTypes.Equity.OwnerEquity);
            this.accounts.Add(ownerAccount);

            var bankCashAccount = new Account(bank, (ushort) AccountTypes.Asset.Cash);
            this.accounts.Add(bankCashAccount);

            var clientMoneyAccount = new Account(bank, (ushort) AccountTypes.Liability.ClientMoney);
            this.accounts.Add(clientMoneyAccount);
            this.ClientMoneyAccount = clientMoneyAccount;

            this.journal.Add(new FinancialTransaction {
                DebitAccount = bankCashAccount,
                CreditAccount = ownerAccount,
                Amount = initialCapital,
                OnDate = DateTime.Now,
            });
        }
        public Account OpenNonCashAccount(Customer customer) {

            customers.Add(customer);
            var customerNonCashAccount = new Account(customer, (ushort) AccountTypes.Asset.NonCash);

            accounts.Add(customerNonCashAccount);

            return customerNonCashAccount;
        }

        public Account OpenNonCashAccount(Customer customer, Money initialAmount) {
            
            var customerNonCashAccount = this.OpenNonCashAccount(customer);

            this.journal.Add(new FinancialTransaction {
                DebitAccount = customerNonCashAccount,
                CreditAccount = this.ClientMoneyAccount,
                Amount = initialAmount,
                OnDate = DateTime.Now,
            });

            return customerNonCashAccount;
        }

        public Money GetAccountBalance(Account account) {

            decimal? credit = this.journal.GetRecords()
                .Where(t => 
                    t.CreditAccount.Equals(account) && 
                    t.Amount.Currency == this.defaultCurrency)
                .Sum(t => t.Amount.Amount);

            decimal? debit = this.journal.GetRecords()
                .Where(t => 
                    t.DebitAccount.Equals(account) && 
                    t.Amount.Currency == this.defaultCurrency)
                .Sum(t => t.Amount.Amount);
            
            return new Money(debit.GetValueOrDefault(0) - credit.GetValueOrDefault(0), this.defaultCurrency);
            // throw new NotImplementedException("GetAccountBalance");
        }
    }
}