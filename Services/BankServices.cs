using BankAccountSimulation.Models;
using System;
using BankAccountSimulation.Contracts;
namespace BankAccountSimulation.Services
{
    internal class BankServices:IBank
    {
        public bool CheckForValidUserNameForUser(Bank bank, string givenUserName)
        {
            foreach(var i in bank.bankUserAccountsArray)
            {
                if(i.UserId== givenUserName) return true;
            }
            return false;
        }
        public bool CheckForValidPasswordForUser(Bank bank,string givenUserName, string givenPassword)
        {
            foreach (var i in bank.bankUserAccountHoldersArray)
            {
                if (i.UserId == givenUserName && i.Password ==givenPassword) return true;
            }
            return false;
        }
        public bool CheckForValidUserNameForStaff(Bank bank, string givenUserName)
        {
            foreach (var i in bank.bankStaffAccountsArray)
            {
                if (i.UserId == givenUserName) return true;
            }
            return false;
        }
        public bool CheckForValidPasswordForStaff(Bank bank, string givenUserName, string givenPassword)
        {
            foreach (var i in bank.bankStaffAccountsArray)
            {
                if (i.UserId == givenUserName && i.Password == givenPassword) return true;
            }
            return false;
        }
        public AccountHolder GetAccountHolder(List<AccountHolder> bankUserAccountHolderArray,string userName)
        {
            foreach(var i in bankUserAccountHolderArray)
            {
                if(i.UserId== userName) return i;
            }
            return null;
        }
        public Account GetAccount(List<Account> bankUserAccountArray,string userName)
        {
            foreach (var i in bankUserAccountArray)
            {
                if (i.UserId == userName) return i;
            }
            return null;
        }
        public void ShowAvailableCurrencies(Bank bank)
        {
            int j = 1;
            foreach(var i in bank.currencies)
            {
                Console.WriteLine(j + ". " + i.CurrencyName);
            }
        }
        public double GetExchangeRate(Bank bank,string currency)
        {
            foreach (var i in bank.currencies)
            {
                if (i.CurrencyName==currency)
                {
                    return i.ExchangeRate;
                }
            }
            return 0;
        }
    }
}
