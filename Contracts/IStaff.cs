using BankAccountSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSimulation.Contracts
{
    internal interface IStaff
    {
        public void CreateUserAccount(List<Account> bankUserAccountsArray, List<AccountHolder> bankUserAccountHolderArray, string userName, string password, double balance);
        public void ChangeServicesChargesForSameBank(Bank bank, double newIMPS, double newRTGS);
        
        public void ChangeServicesChargesForDifferentBank(Bank bank, double newIMPS, double newRTGS);
        
        public void AddNewCurrencyAndExchangeRate(Bank bank, string newCurrency, double newExchangeRate);
        
        public void UpdateUserAccount(Account account, string newUserName, string newPassword, double newBal);
        
        public void UpdateUserAccountHolder(AccountHolder accountHolder, string newUserName, string newPassword, double newBal);
        
        public void DeleteAccount(List<Account> accountsArray, Account account);
        
        public void DeleteAccountHolder(List<AccountHolder> accountHoldersArray, AccountHolder accountHolder);
        
        public void ShowTranscationHistory(Account account);
        
        public bool RevertTranscation(Account senderAccount, Account receiverAccount, string transcationId);
        public string GetReceiver(Account account, string txnId);
        public string GetReceiverBank(Account account, string txnId);


    }
}
