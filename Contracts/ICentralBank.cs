using BankAccountSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSimulation.Contracts
{
    internal interface ICentralBank
    {
        public void BankCreation(CentralBank centralBank, string bankName);
        public void DefaultStaffAccountCreation(CentralBank centralBank, string bankName);
        public Bank GetBank(CentralBank centralBank, string bankName);

    }
}
