using System.Collections.Generic;
using BankAPI.Interfaces;
using System;
using System.Linq;
using BankAPI.DefaultImplementations;

namespace BankAPI.Model {

    public class Bank {

        // static methods
        public static Bank CreateInMemoryBank(string defaultCurrency, decimal seedCapital)
        {
            var customers = new GenericInMemoryRepository<Customer>();
            var accounts = new GenericInMemoryRepository<Account>();
            var journal = new GenericInMemoryRepository<FinancialTransaction>();

            var bank = new Bank(defaultCurrency, seedCapital, customers, accounts, journal);

            return bank;
        }

        private string defaultCurrency;
        private readonly Account NonCashAssetAccount;
        private readonly Account Payables;
        private readonly Account Receivables;
        private Money equity;

        /// <summary> Investment of assets in a business by the owner or owners is called <c>capital</c>. </summary>
        public Money Equity {
            get { return equity; }
        }

        public string DefaultCurrency
        {
            get { return defaultCurrency; }
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

            var ownerAccount = new Account(this, owner, (ushort) AccountTypes.Equity.OwnerEquity);
            this.accounts.Add(ownerAccount);

            var bankCashAccount = new Account(this, bank, (ushort) AccountTypes.Asset.Cash);
            this.accounts.Add(bankCashAccount);

            var nonCashAssetAccount = new Account(this, bank, (ushort) AccountTypes.Asset.NonCash);
            this.accounts.Add(nonCashAssetAccount);
            this.NonCashAssetAccount = nonCashAssetAccount;

            var payables = new Account(this, bank, (ushort) AccountTypes.Liability.AccountsPayable);
            this.accounts.Add(payables);
            this.Payables = payables;

            var receivables = new Account(this, bank, (ushort)AccountTypes.Asset.AccountsReceivable);
            this.accounts.Add(receivables);
            this.Receivables = receivables;

            this.journal.Add(new FinancialTransaction {
                DebitAccount = bankCashAccount,
                CreditAccount = ownerAccount,
                Amount = initialCapital,
                OnDate = DateTime.Now,
            });
        }
        public Account OpenClientAccount(Customer customer) {

            customers.Add(customer);
            var customerClientMoneyAccount = new Account(this, customer, (ushort) AccountTypes.Liability.ClientFunds);

            accounts.Add(customerClientMoneyAccount);

            return customerClientMoneyAccount;
        }

        public Account OpenClientAccount(Customer customer, Money initialAmount) {
            
            var customerClientMonryAccount = this.OpenClientAccount(customer);

            this.journal.Add(new FinancialTransaction {
                DebitAccount = this.NonCashAssetAccount,
                CreditAccount = customerClientMonryAccount,
                Amount = initialAmount,
                OnDate = DateTime.Now,
            });

            return customerClientMonryAccount;
        }

        public Account OpenClientAccount(Customer customer, decimal initialAmount)
        {
            return this.OpenClientAccount(customer, new Money(initialAmount, this.defaultCurrency));
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
            
            return new Money(Math.Abs(debit.GetValueOrDefault(0) - credit.GetValueOrDefault(0)), this.defaultCurrency);
            // throw new NotImplementedException("GetAccountBalance");
        }

        public Money GetAccountTypeBalance<T>() where T : Enum
        {
            decimal? credit = this.journal.GetRecords()
                .Where(t => Enum.IsDefined(typeof(T), Enum.ToObject(typeof(T), t.CreditAccount.AccountType)))
                .Sum(t => t.Amount.Amount);

            decimal? debit = this.journal.GetRecords()
                  .Where(t => Enum.IsDefined(typeof(T), Enum.ToObject(typeof(T), t.DebitAccount.AccountType)))
                  .Sum(t => t.Amount.Amount);

            return new Money(Math.Abs(debit.GetValueOrDefault(0) - credit.GetValueOrDefault(0)), this.defaultCurrency);
            // throw new NotImplementedException("GetAssetsBalance");
        }

        public void DepositNonCash(Account account, Money amount) {

            if (account.AccountType != (ushort) AccountTypes.Liability.ClientFunds)
                throw new ArgumentException("Account type should be Liability.ClientFunds"); 

            this.journal.Add(new FinancialTransaction {
                DebitAccount = this.NonCashAssetAccount,
                CreditAccount = account,
                Amount = amount,
                OnDate = DateTime.Now,
            });
        }

        public void SendMoneyOutside(Account sender, Money amount)
        {
            if (sender.AccountType != (ushort)AccountTypes.Liability.ClientFunds)
                throw new ArgumentException("Sender's account type should be Liability.ClientFunds");

            this.journal.Add(new FinancialTransaction
            {
                DebitAccount = sender,
                CreditAccount = this.Payables,
                Amount = amount,
                OnDate = DateTime.Now,
            });
        }

        public void ReceiveMoneyFromOutside(Account receiver, Money amount)
        {
            if (receiver.AccountType != (ushort)AccountTypes.Liability.ClientFunds)
                throw new ArgumentException("Sender's account type should be Liability.ClientFunds");

            this.journal.Add(new FinancialTransaction
            {
                DebitAccount = this.Receivables,
                CreditAccount = receiver,
                Amount = amount,
                OnDate = DateTime.Now,
            });
        }
    }
}