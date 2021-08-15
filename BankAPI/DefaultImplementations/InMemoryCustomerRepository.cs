using System.Collections;
using System.Collections.Generic;
using BankAPI.Interfaces;
using BankAPI.Model;

namespace BankAPI.DefaultImplementations
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private List<Customer> customers = new List<Customer>();
        
        void ICustomerRepository.Add(Customer customer)
        {
            this.customers.Add(customer);
        }

        void ICustomerRepository.Remove(Customer customer)
        {
            this.customers.Remove(customer);
        }

        IEnumerable ICustomerRepository.GetCustomers()
        {
            return customers.ToArray();
        }
    }
}