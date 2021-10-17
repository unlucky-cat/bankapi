using System.Collections.Generic;
using BankAPI.Interfaces;
using System;
using System.Linq;
using BankAPI.DefaultImplementations;

namespace BankAPI.Model {

    public class Bank {

        private string defaultCurrency;
        private readonly Account NonCashAssetAccount;
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

            var nonCashAssetAccount = new Account(bank, (ushort) AccountTypes.Asset.NonCash);
            this.accounts.Add(nonCashAssetAccount);
            this.NonCashAssetAccount = nonCashAssetAccount;

            this.journal.Add(new FinancialTransaction {
                DebitAccount = bankCashAccount,
                CreditAccount = ownerAccount,
                Amount = initialCapital,
                OnDate = DateTime.Now,
            });
        }
        public Account OpenClientMoneyAccount(Customer customer) {

            customers.Add(customer);
            var customerClientMoneyAccount = new Account(customer, (ushort) AccountTypes.Liability.ClientMoney);

            accounts.Add(customerClientMoneyAccount);

            return customerClientMoneyAccount;
        }

        public Account OpenClientMoneyAccount(Customer customer, Money initialAmount) {
            
            var customerClientMonryAccount = this.OpenClientMoneyAccount(customer);

            this.journal.Add(new FinancialTransaction {
                DebitAccount = this.NonCashAssetAccount,
                CreditAccount = customerClientMonryAccount,
                Amount = initialAmount,
                OnDate = DateTime.Now,
            });

            return customerClientMonryAccount;
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

        public Account DepositNonCash(Account account, Money amount) {

            if (account.AccountType != (ushort) AccountTypes.Liability.ClientMoney)
                throw new ArgumentException("Account type should be Liability.ClientMoney"); 

            this.journal.Add(new FinancialTransaction {
                DebitAccount = this.NonCashAssetAccount,
                CreditAccount = account,
                Amount = amount,
                OnDate = DateTime.Now,
            });

            return account;
        }
    }
}