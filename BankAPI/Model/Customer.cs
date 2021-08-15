using System;

namespace BankAPI.Model {
    public class Customer {
        
        private Guid uid;
        public Customer() {

            this.uid = Guid.NewGuid();
        }

        public override string ToString()
        {
            return uid.ToString();
        }
    }
}