using System.Collections.Generic;
using BankAPI.Model;

namespace BankAPI.Interfaces
{
    public interface ITransactionsJournalRepository 
    {        
        IEnumerable<FinancialTransaction> GetTransactions();      
        void Add(FinancialTransaction transaction);        
        void Remove(FinancialTransaction transaction);           
    }
}