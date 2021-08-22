using System;

namespace BankAPI.Model
{
    public class GeneralLedgerRecord {

            public Account DebitAccount { get; set; }
            public Account CreditAccount { get; set; }
            public DateTime OnDate { get; set; }
            public Money Amount  { get; set; }
    }
}