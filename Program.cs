using System;
using BankAPI.Model;

namespace BankAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var bank = new Bank("EUR");

            var acc = bank.OpenAccount(new Person());

            Console.WriteLine(acc);
            Console.ReadKey();
        }
    }
}
