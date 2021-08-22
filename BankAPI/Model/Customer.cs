using System;

namespace BankAPI.Model {
    public class Customer {
        
        private Guid uid;

        public string Name { get; set; }
        public Customer(string name) {

            this.Name = name;
            this.uid = Guid.NewGuid();

        }

        public override string ToString()
        {
            return $"{Name} : {uid}";
        }
    }
}