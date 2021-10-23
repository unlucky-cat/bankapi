using System;
using System.Linq;
using BankAPI.DefaultImplementations;
using BankAPI.Interfaces;
using BankAPI.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSUnitTests
{
    [TestClass]
    public class Real_life_cases
    {
        [TestMethod]
        public void Jack_is_opening_an_account_and_deposit_his_savings_to_it()
        {
            decimal actual_capital = 1000000m;
            var bank = Bank.CreateInMemoryBank("EUR", actual_capital);

            var jack = new Customer("Jack");
            decimal actual_savings = 100m;
            var acc = bank.OpenClientAccount(jack, actual_savings);

            var jack_savings_balance_amount = acc.GetBalance().Amount;

            Assert.IsTrue(actual_savings == jack_savings_balance_amount
                , "After depositing his savings to the newly created account Jack should have ({0}) Euros on his account, but he has {1}"
                , actual_savings, jack_savings_balance_amount);


            var assets = bank.GetAccountTypeBalance<AccountTypes.Asset>().Amount;
            Assert.IsTrue(actual_capital + actual_savings == assets, "We expect assets balance to be = {0} + {1}, but it is {2}", actual_capital, actual_savings, assets);

            var liabilities = bank.GetAccountTypeBalance<AccountTypes.Liability>().Amount;
            Assert.IsTrue(actual_savings == liabilities, "We expect liabilities balance to be = {0}, but it is {1}", actual_savings, liabilities);

            var equity = bank.GetAccountTypeBalance<AccountTypes.Equity>().Amount;
            Assert.IsTrue(actual_capital == equity, "We expect equity balance to be = {0}, but it is {1}", actual_capital, equity);

            Assert.IsTrue(assets == liabilities + equity, "We expect equity accounting equation to be 1000100 = 100 + 1000000, but it is {0} = {1} + {2}");

            // ******************************************************************
            //     OPERATION   |  ASSET    =    LIABILITY    +    EQUITY        *
            //  -------------- | ---------------------------------------------- *
            //                 |                                                *
            //    FOUNDATION   |  +1000000                         +1000000     *
            //    DEPOSIT      |      +100            +100                      *
            //                 |                                                *
            //  -------------- | ---------------------------------------------- *
            //          TOTAL: |  1000100  =         100     +    1000000       *
            // ******************************************************************

        }

        [TestMethod]
        public void Jack_adds_money_to_an_existing_account_and_sends_all_his_money_to_his_grandma_in_the_Israel()
        {
            decimal actual_capital = 1000000m;
            var bank = Bank.CreateInMemoryBank("EUR", actual_capital);

            var jack = new Customer("Jack");
            decimal
                    existing_amount = 100m,
                    missing_amount = 100m,
                    amount_to_send = existing_amount + missing_amount;

            var acc = bank
                .OpenClientAccount(jack, existing_amount)
                .DepositNonCash(missing_amount)
                .SendMoneyOutside(amount_to_send);


            var expecting_zero_balance_here = acc.GetBalance().Amount;

            Assert.IsTrue(0 == expecting_zero_balance_here, "We expect Jack's balance to be 0 after he sent all his savings to grandma, but there are {0}", expecting_zero_balance_here);

            decimal
                    expected_assets = 1000200m,
                    expected_liabilities = 200m,
                    expected_equity = 1000000m;

            var actual_assets = bank.GetAccountTypeBalance<AccountTypes.Asset>().Amount;
            Assert.IsTrue(expected_assets == actual_assets, "We expect assets balance to be = {0}, but it is {1}", expected_assets, actual_assets);

            var actual_liabilities = bank.GetAccountTypeBalance<AccountTypes.Liability>().Amount;
            Assert.IsTrue(expected_liabilities == actual_liabilities, "We expect liabilities balance to be = {0}, but it is {1}", expected_liabilities, actual_liabilities);

            var actual_equity = bank.GetAccountTypeBalance<AccountTypes.Equity>().Amount;
            Assert.IsTrue(expected_equity == actual_equity, "We expect equity balance to be = {0}, but it is {1}", expected_equity, actual_equity);

            Assert.IsTrue(actual_assets == actual_liabilities + actual_equity, "We expect equity accounting equation to be {0} = {1} + {2}, but it is {3} = {4} + {5}"
                , expected_equity, expected_liabilities, expected_assets, actual_equity, actual_liabilities, actual_assets);

            // throw new NotImplementedException("Jack_adds_money_to_an_existing_account_and_sends_all_his_money_to_his_grandma_in_the_Israel");

            // ******************************************************************
            //     OPERATION   |  ASSET    =    LIABILITY    +    EQUITY        *
            //  -------------- | ---------------------------------------------- *
            //                 |                                                *
            //    FOUNDATION   |  +1000000                         +1000000     *
            //    EXISTING     |      +100       +100                           *
            //    MISSING      |      +100       +100                           *
            //    SENDING      |                 +200/-200                      *
            //                 |                                                *
            //  -------------- | ---------------------------------------------- *
            //          TOTAL: |   1000200  =     200         +     1000000     *
            // ******************************************************************
        }
    }
}
