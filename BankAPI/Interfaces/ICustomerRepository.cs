using System.Collections;
using BankAPI.Model;

namespace BankAPI.Interfaces
{
    public interface ICustomerRepository 
    {        
        IEnumerable GetCustomers();        
        //Customer GetCustomerByID(int customerId);        
        void Add(Customer customer);        
        void Remove(Customer customer);        
        //void UpdateCustomer(Customer customer);        
        //void Save();    
    }
}