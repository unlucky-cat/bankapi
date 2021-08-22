using System;
using System.Collections.Generic;

namespace BankAPI.Model
{
    public static class GeneralLedgerDefinition {
    
        public enum Category : ushort
        {
            Accounts = 100,
            Asset = 200,
            Liability = 300, 
            Equity = 400, 
            Expense = 500, 
            Revenue = 600,
        }

        public enum Liability : ushort
        {
            ClientMoney = 305,
        }

        public enum Equity : ushort 
        {
            OwnerEquity = 405,
        }

        public enum Asset : ushort 
        {
            Cash = 205,
            NonCash = 210,
            AccountsReceivable = 220,
        }
    }
}