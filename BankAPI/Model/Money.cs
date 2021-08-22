using System;

namespace BankAPI.Model
{
    public class Money {
        public Money(float Amount, string Currency) {

            if (string.IsNullOrWhiteSpace(Currency)) 
                throw new ArgumentException("Currency can't be empty string", nameof(Currency));
                
            this.Amount = Amount;
            this.Currency = Currency;
        }
        public float Amount {get; set;}
        public string Currency {get; set;}

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}