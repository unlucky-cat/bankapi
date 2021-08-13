using System;
using BankAPI.Model;

namespace BankAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var bank = new Bank();

            bank.OpenAccount(new Person());

            Console.WriteLine("Hello World!");
        }
    }
}
