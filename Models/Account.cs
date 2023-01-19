using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSimulation.Models
{
    internal class Account
    {
        public string UserId { get; set; }
        
        public string AccountId { get; set; }
        public double AccountBalance { get; set; }
        
        public List<Transcation> transcations= new List<Transcation>(); 
    }
}
