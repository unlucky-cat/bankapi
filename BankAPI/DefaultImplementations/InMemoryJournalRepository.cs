using System.Collections;
using System.Collections.Generic;
using BankAPI.Interfaces;
using BankAPI.Model;

namespace BankAPI.DefaultImplementations
{
    public class InMemoryJournalRepository : ITransactionsJournalRepository
    {
        private List<FinancialTransaction> transactions = new List<FinancialTransaction>();
        
        void ITransactionsJournalRepository.Add(FinancialTransaction transaction)
        {
            this.transactions.Add(transaction);
        }

        void ITransactionsJournalRepository.Remove(FinancialTransaction transaction)
        {
            this.transactions.Remove(transaction);
        }

        IEnumerable<FinancialTransaction> ITransactionsJournalRepository.GetTransactions()
        {
            return transactions.ToArray();
        }
    }
}