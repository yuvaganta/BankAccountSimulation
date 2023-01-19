using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSimulation.Models
{
    internal class Bank
    {
        public string BankName { get; set; }
        public string BankId { get; set; }
        public double RTGSSame { get; set; }

        public double IMPSSame { get; set; }
        public double RTGSDifferent { get; set; }
        public double IMPSDifferent { get; set; }
        
       
        public List<Currency> currencies = new List<Currency>();
        public List<Account> bankUserAccountsArray = new List<Account>();
        public List<Staff> bankStaffAccountsArray = new List<Staff>();
        public List<AccountHolder> bankUserAccountHoldersArray = new List<AccountHolder>();

        
        public Bank()
        {
           
            RTGSSame = 0;
            IMPSSame = 5;
            RTGSDifferent = 2;
            IMPSDifferent = 6;
            currencies.Add(new Currency() { ExchangeRate = 1, CurrencyName = "INR" });
        }
        
    }
}
