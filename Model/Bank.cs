using System.Collections.Generic;

namespace BankAPI.Model {
    public class Bank {

        private List<Person> persons;
        public Bank() {

            this.persons = new List<Person>();
        }

        public void OpenAccount(Person person) {
            persons.Add(person);
        }
    }
}