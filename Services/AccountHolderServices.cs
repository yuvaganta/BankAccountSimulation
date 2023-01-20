using BankAccountSimulation.Models;
using System;
using BankAccountSimulation.Contracts;

namespace BankAccountSimulation.Services
{
    internal class AccountHolderServices:IAccountHolder
    {
        public void Deposit(Account senderAccount,Account receiverAccount,double amount,string txnId,string senderBankName,string receiverBankName) {
            senderAccount.AccountBalance += amount;
            string tempTranscationDetails=txnId+"    +"+amount+"   aval.bal. " + senderAccount.AccountBalance + " sendfrom" + receiverAccount.UserId + " receivedto" + senderAccount.UserId;
            senderAccount.transcations.Add(new Transcation() { Amount= amount,TranscationID=txnId,TranscationDetails=tempTranscationDetails,Sender=receiverAccount.UserId,Receiver=senderAccount.UserId,SenderBankName=receiverBankName,ReceiverBankName=senderBankName });
        }
        public bool Withdraw(Account senderAccount,Account receiverAccount ,double amount,string txnId, string senderbankName, string receiverBankName) {
            if (senderAccount.AccountBalance < amount)
            {
                Console.WriteLine("***Insufficent Funds***");
                return false;
            }
            else
            {
                senderAccount.AccountBalance -= amount;
            string tempTranscationDetails = txnId + "    -" + amount + "   aval.bal. " + senderAccount.AccountBalance+" sendfrom"+senderAccount.UserId+" receivedto"+receiverAccount.UserId;
                senderAccount.transcations.Add(new Transcation() { Amount = amount, TranscationID = txnId, TranscationDetails = tempTranscationDetails,Sender=senderAccount.UserId,Receiver=receiverAccount.UserId,SenderBankName=senderbankName,ReceiverBankName=receiverBankName });
                return true;
            }
        }
        public void Transfer(Account senderAccount, Account receiverAccount, double credittingAmount, double debittingAmount, string txnId, string senderbankName, string receiverBankName)
        {
            AccountHolderServices accountHolderServices = new AccountHolderServices();
            bool temp = accountHolderServices.Withdraw(senderAccount,receiverAccount, debittingAmount, txnId,  senderbankName,  receiverBankName);
            if(temp)
            {
                accountHolderServices.Deposit(receiverAccount, senderAccount, credittingAmount, txnId, receiverBankName, senderbankName);
            }
        }
        public void ShowTranscationHistory(Account account) {
        foreach(var i in account.transcations)
            {
                Console.WriteLine(i.TranscationDetails);
            }
        }
    }
}
