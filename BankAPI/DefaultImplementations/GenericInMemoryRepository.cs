using System.Collections;
using System.Collections.Generic;
using BankAPI.Interfaces;
using BankAPI.Model;

namespace BankAPI.DefaultImplementations
{
    public class GenericInMemoryRepository<T> : IGenericRepository<T>
    {
        private List<T> records = new List<T>();
        
        void IGenericRepository<T>.Add(T record)
        {
            this.records.Add(record);
        }

        void IGenericRepository<T>.Remove(T record)
        {
            this.records.Remove(record);
        }

        IEnumerable<T> IGenericRepository<T>.GetRecords()
        {
            return records.ToArray();
        }
    }
}