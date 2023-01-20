using BankAccountSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSimulation.Contracts
{
    internal interface IBank
    {
        public bool CheckForValidUserNameForUser(Bank bank, string givenUserName);
        public bool CheckForValidPasswordForUser(Bank bank, string givenUserName, string givenPassword);
        public bool CheckForValidUserNameForStaff(Bank bank, string givenUserName);
        public bool CheckForValidPasswordForStaff(Bank bank, string givenUserName, string givenPassword);
        public AccountHolder GetAccountHolder(List<AccountHolder> bankUserAccountHolderArray, string userName);
        public Account GetAccount(List<Account> bankUserAccountArray, string userName);
        public void ShowAvailableCurrencies(Bank bank);
        public double GetExchangeRate(Bank bank, string currency);

    }
}
