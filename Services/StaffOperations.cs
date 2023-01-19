using System;
using System.Collections.Generic;
using BankAccountSimulation.Models;

namespace BankAccountSimulation.Services
{
    internal class StaffOperations
    {
        public void CreateUserAccount(List<Account> bankUserAccountsArray, List<AccountHolder> bankUserAccountHolderArray, string userName,string password,double balance)
        {
            string temp = userName.Substring(0, 3) + DateTime.Now.Ticks;
            bankUserAccountsArray.Add(new Account() { UserId = userName, AccountBalance = 0, AccountId = temp });
            bankUserAccountHolderArray.Add(new AccountHolder() { UserId = userName, Password = password, AccountId = temp });
            
        }
        public void ChangeServicesChargesForSameBank(Bank bank,double newIMPS,double newRTGS)
        {
            bank.RTGSSame = newRTGS;
            bank.IMPSSame = newIMPS;
        }
        public void ChangeServicesChargesForDifferentBank(Bank bank, double newIMPS, double newRTGS)
        {
            bank.IMPSDifferent = newIMPS;
            bank.RTGSDifferent = newRTGS;
        }
        public void AddNewCurrencyAndExchangeRate(Bank bank,string newCurrency,double newExchangeRate)
        {
            bank.currencies.Add(new Currency() { CurrencyName = newCurrency, ExchangeRate = newExchangeRate });
            
        }
        public void UpdateUserAccount(Account account, string newUserName, string newPassword, double newBal)
        {
            
                account.UserId = newUserName;
                account.AccountBalance = newBal;
        }
        public void UpdateUserAccountHolder(AccountHolder accountHolder,  string newUserName, string newPassword, double newBal)
        {
            accountHolder.UserId = newUserName;
            accountHolder.Password = newPassword;
        }
        public void DeleteAccount(List<Account> accountsArray,Account account)
        {
            accountsArray.Remove(account);
        }
        public void DeleteAccountHolder(List<AccountHolder> accountHoldersArray, AccountHolder accountHolder)
        {
            accountHoldersArray.Remove(accountHolder);
        }
        public void ShowTranscationHistory(Account account)
        {
            foreach (var i in account.transcations)
            {
                Console.WriteLine(i.TranscationDetails);
            }
        }
        public bool RevertTranscation(Account senderAccount,Account receiverAccount,string transcationId)
        {
            BankServices bankServices = new BankServices();
            foreach(var i in senderAccount.transcations)
            {
                if (i.TranscationID == transcationId)
                {
                    senderAccount.AccountBalance += i.Amount;
                    receiverAccount.AccountBalance -= i.Amount;      
                        return true; 
                }
            }
            return false;
        }
        public string GetReceiver(Account account,string txnId)
        {
            foreach(var i in account.transcations)
            {
                if (i.TranscationID == txnId)
                {
                    return i.Receiver;
                }
            }
            return "";
        }
        public string GetReceiverBank(Account account, string txnId)
        {
            foreach (var i in account.transcations)
            {
                if (i.TranscationID == txnId)
                {
                    return i.ReceiverBankName;
                }
            }
            return "";
        }
    }
}
