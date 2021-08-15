using System.Collections.Generic;

namespace BankAPI.Model
{
    public class Account
    {
        private List<Customer> customers;
        private Money money;
        public Money Balance {
            get { return this.money; }
        }
        public Account(Customer customer, Money money) {

            this.customers = new List<Customer>();
            this.customers.Add(customer);

            this.money = money;
        }

        public override string ToString()
        {
            return string.Format("{0} has {1}", customers[0], money);
        }
    }
}