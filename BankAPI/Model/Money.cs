using System;

namespace BankAPI.Model
{
    public class Money {
        public Money(decimal Amount, string Currency) {

            if (string.IsNullOrWhiteSpace(Currency)) 
                throw new ArgumentException("Currency can't be empty string", nameof(Currency));
                
            this.Amount = Amount;
            this.Currency = Currency;
        }
        public decimal Amount {get; set;}
        public string Currency {get; set;}

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}