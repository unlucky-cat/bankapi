namespace BankAPI.Model
{
    public class Money {
        public Money(float Amount, string Currency) {
            this.Amount = Amount;
            this.Currency = Currency;
        }
        public float Amount {get; set;}
        public string Currency {get; set;}

        public override string ToString()
        {
            return string.Format("{0} {1}", Amount, Currency);
        }
    }
}