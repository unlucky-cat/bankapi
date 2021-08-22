using System.Collections.Generic;
using BankAPI.Interfaces;
using System;

namespace BankAPI.Model {

    public class Bank {

        private string defaultCurrency;
        private readonly Account ClientMoneyAccount;
        private Money equity;

        /// <summary> Investment of assets in a business by the owner or owners is called <c>capital</c>. </summary>
        public Money Equity {
            get { return equity; }
        }
        private ICustomerRepository customers;
        private IAccountRepository accounts;

        private List<GeneralLedgerRecord> journal = new List<GeneralLedgerRecord>();
        
        public Bank(
            string defaultCurrency, 
            float seedCapital,
            ICustomerRepository customerRepository, 
            IAccountRepository accountRepository
        ) {
            
            var initialCapital = new Money(seedCapital, defaultCurrency);
            this.defaultCurrency = defaultCurrency;
            this.equity = initialCapital;
            this.customers = customerRepository;
            this.accounts = accountRepository;

            var owner = new Customer("The Owner");
            this.customers.Add(owner);

            var bank = new Customer("The Bank");
            this.customers.Add(bank);

            var ownerAccount = new Account(owner, (ushort) GeneralLedgerDefinition.Equity.OwnerEquity);
            this.accounts.Add(ownerAccount);

            var bankCashAccount = new Account(bank, (ushort) GeneralLedgerDefinition.Asset.Cash);
            this.accounts.Add(bankCashAccount);

            var clientMoneyAccount = new Account(bank, (ushort) GeneralLedgerDefinition.Asset.Cash);
            this.accounts.Add(clientMoneyAccount);
            this.ClientMoneyAccount = clientMoneyAccount;

            this.journal.Add(new GeneralLedgerRecord {
                DebitAccount = bankCashAccount,
                CreditAccount = ownerAccount,
                Amount = initialCapital,
                OnDate = DateTime.Now,
            });
        }
        public Account OpenAccount(Customer customer) {

            customers.Add(customer);
            var acc = new Account(customer, (ushort) GeneralLedgerDefinition.Asset.NonCash);

            accounts.Add(acc);

            return acc;
        }

        public Account OpenAccount(Customer customer, Money initialAmount) {
            
            var customerAccount = this.OpenAccount(customer);

            this.journal.Add(new GeneralLedgerRecord {
                DebitAccount = customerAccount,
                CreditAccount = this.ClientMoneyAccount,
                Amount = initialAmount,
                OnDate = DateTime.Now,
            });

            return customerAccount;
        }
    }
}