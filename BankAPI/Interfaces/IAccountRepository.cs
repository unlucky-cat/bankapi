using System.Collections;
using BankAPI.Model;

namespace BankAPI.Interfaces
{
    public interface IAccountRepository 
    {        
        IEnumerable GetAccounts();      
        void Add(Account account);        
        void Remove(Account account);           
    }
}