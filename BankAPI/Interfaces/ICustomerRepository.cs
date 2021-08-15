using System.Collections;
using BankAPI.Model;

public interface ICustomerRepository 
{        
    IEnumerable GetCustomers();        
    Customer GetCustomerByID(int customerId);        
    void InsertCustomer(Customer customer);        
    void DeleteCustomer(int customerId);        
    void UpdateCustomer(Customer customer);        
    void Save();    
}