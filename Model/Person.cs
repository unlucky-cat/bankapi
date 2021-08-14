using System;

namespace BankAPI.Model {
    public class Person {
        
        private Guid uid;
        public Person() {

            this.uid = Guid.NewGuid();
        }

        public override string ToString()
        {
            return uid.ToString();
        }
    }
}