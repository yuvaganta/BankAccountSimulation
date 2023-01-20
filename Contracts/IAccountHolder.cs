using BankAccountSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSimulation.Contracts
{
    internal interface IAccountHolder
    {
        public void Deposit(Account senderAccount, Account receiverAccount, double amount, string txnId, string senderBankName, string receiverBankName);
        public bool Withdraw(Account senderAccount, Account receiverAccount, double amount, string txnId, string senderbankName, string receiverBankName); 
        public void Transfer(Account senderAccount, Account receiverAccount, double credittingAmount, double debittingAmount, string txnId, string senderbankName, string receiverBankName);
        public void ShowTranscationHistory(Account account);


    }

}
