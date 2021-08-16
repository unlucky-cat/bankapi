using System.Collections;
using System.Collections.Generic;
using BankAPI.Interfaces;
using BankAPI.Model;

namespace BankAPI.DefaultImplementations
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private List<Account> accounts = new List<Account>();
        
        void IAccountRepository.Add(Account account)
        {
            this.accounts.Add(account);
        }

        void IAccountRepository.Remove(Account account)
        {
            this.accounts.Remove(account);
        }

        IEnumerable IAccountRepository.GetAccounts()
        {
            return accounts.ToArray();
        }
    }
}