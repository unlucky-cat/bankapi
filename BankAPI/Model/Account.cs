using System.Collections.Generic;

namespace BankAPI.Model
{
    public class Account
    {
        private Customer customer;
        public ushort AccountType { get; set; }
        public Account(Customer customer, ushort accountType) {

            this.customer = customer;
            this.AccountType = accountType;
        }

        public override string ToString()
        {
            //return $"{customer} has {money}";
            //return string.Format("{0} has {1}", customer, money);

            return base.ToString();
        }
    }
}