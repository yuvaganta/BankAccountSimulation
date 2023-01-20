using BankAccountSimulation.Models;
using System;
using BankAccountSimulation.Contracts;

namespace BankAccountSimulation.Services
{
    internal class CentralBankServices :ICentralBank
    {
        public void BankCreation(CentralBank centralBank, string bankName)
        {
            centralBank.banksArray.Add(new Bank() { BankName = bankName, BankId = bankName.Substring(0, 3) + DateTime.Now.Ticks });
        }
        public void DefaultStaffAccountCreation(CentralBank centralBank, string bankName)
        {
                foreach(var i in centralBank.banksArray)
                 {
                if (i.BankName == bankName)
                {
                    string temp=i.BankName.Substring(0,3)+DateTime.Now.Ticks;
                    i.bankStaffAccountsArray.Add(new Staff() { UserId = bankName, Password = bankName });
                    break;
                }
            }
        }
        public Bank GetBank(CentralBank centralBank, string bankName)
        {
            foreach(var i in centralBank.banksArray)
            {
                if (i.BankName == bankName) { return i; }
            }
            return null; 
        }
    } }
