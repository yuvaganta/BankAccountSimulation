using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSimulation.Models
{
    internal class Transcation
    {
        public string TranscationID { get; set; }
        public double Amount { get; set; }
        public string TranscationDetails { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string SenderBankName { get; set; }
        public string ReceiverBankName { get; set; }
    }
}
