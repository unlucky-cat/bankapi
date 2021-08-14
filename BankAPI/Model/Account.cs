using System.Collections.Generic;

namespace BankAPI.Model
{
    public class Account
    {
        private List<Person> persons;
        private Money money;
        public Money Balance {
            get { return this.money; }
        }
        public Account(Person person, Money money) {

            this.persons = new List<Person>();
            this.persons.Add(person);

            this.money = money;
        }

        public override string ToString()
        {
            return string.Format("{0} has {1}", persons[0], money);
        }
    }
}