using System.Collections.Generic;

namespace BankAPI.Model
{
    public class Account
    {
        private Customer customer;
        private Money money;
        public Money Balance {
            get { return this.money; }
        }
        public Account(Customer customer, Money money) {

            this.customer = customer;
            this.money = money;
        }

        public override string ToString()
        {
            return string.Format("{0} has {1}", customer, money);
        }
    }
}