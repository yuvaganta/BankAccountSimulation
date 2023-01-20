using BankAccountSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSimulation
{
    internal class PrintingAvailableBanks
    {
        public void AvailableBanksReturn(CentralBank centralBank)
        {
            int index = 1;
            foreach(var bank in centralBank.banksArray)
            {
                Console.WriteLine(index + ". " + bank.BankName);
                index++;
            }
        }
    }
}
