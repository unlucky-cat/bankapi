using System.Collections.Generic;
using BankAPI.Model;

namespace BankAPI.Interfaces
{
    public interface IGenericRepository<T> 
    {        
        IEnumerable<T> GetRecords();      
        void Add(T record);        
        void Remove(T record);           
    }
}