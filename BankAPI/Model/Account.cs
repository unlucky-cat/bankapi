using System.Collections.Generic;

namespace BankAPI.Model
{
    public class Account
    {
        private Customer customer;
        private Bank bank;
        public ushort AccountType { get; set; }
        public Account(Bank bank, Customer customer, ushort accountType) {

            this.bank = bank;
            this.customer = customer;
            this.AccountType = accountType;
        }

        public Money GetBalance()
        {
            return this.bank.GetAccountBalance(this);
        }

        public Account DepositNonCash(Money amount)
        {
            bank.DepositNonCash(this, amount);

            return this;
        }

        public Account DepositNonCash(decimal amount)
        {
            return this.DepositNonCash(new Money(amount, bank.DefaultCurrency));
        }

        public Account SendMoneyOutside(Money amount)
        {
            bank.SendMoneyOutside(this, amount);

            return this;
        }

        public Account SendMoneyOutside(decimal amount)
        {
            return this.SendMoneyOutside(new Money(amount, bank.DefaultCurrency));
        }

        public Account ReceiveMoneyFromOutside(Money amount)
        {
            bank.ReceiveMoneyFromOutside(this, amount);

            return this;
        }

        public Account ReceiveMoneyFromOutside(decimal amount)
        {
            return this.ReceiveMoneyFromOutside(new Money(amount, bank.DefaultCurrency));
        }

        public override string ToString()
        {
            //return $"{customer} has {money}";
            //return string.Format("{0} has {1}", customer, money);

            return base.ToString();
        }
    }
}