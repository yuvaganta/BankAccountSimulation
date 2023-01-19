using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankAccountSimulation
{
    public class Banks
    {/*
        public string BankName { get; set; }
        public string BankId { get; set; }
        public float RTGSsame { get; set; }

        public float IMPSsame { get; set; }
        public float RTGSdifferent { get; set; }
        public float IMPSdifferent { get; set; }
        public string acceptedCurrency { get; set; }
        public double currencyExchangeRate { get; set; }
        public List<UserAccount> bankUserAccountsArray=new List<UserAccount>();
        public List<BankStaffAccount> bankStaffAccountsArray=new List<BankStaffAccount>();
        public Banks()
        {
            RTGSsame= 0;
            IMPSsame= 5;
            RTGSdifferent= 2;
            IMPSdifferent= 6;
            acceptedCurrency = "INR";
        }
        public void defaultUserAccountCreation()
        {
            this.bankStaffAccountsArray.Add(new BankStaffAccount() { userid = this.BankName, password = this.BankName });
        }
        public bool checkForValidUserNameForUser(string givenuserName)
        {
            for (int k = 0; k < this.bankUserAccountsArray.Count; k++)
            {
                if (this.bankUserAccountsArray[k].userid == givenuserName) return true;
            }
            return false;
        }

        public bool checkForValidPasswordForUser(string givenusername, string givenpassword)
        {
            int flag = getIndex(givenusername);
            if (this.bankUserAccountsArray[flag].password == givenpassword) return true;
            else return false;
        }
        public bool checkForValidUserNameForStaff(string givenusername)
        {
            for(int k =0;k<this.bankStaffAccountsArray.Count;k++)
            {
                if (this.bankStaffAccountsArray[k].userid==givenusername) return true;
            }
            return false;
        }
        public bool checkForValidPasswordForStaff(string givenusername,string givenpassword)
        {
            int flag = -1;
            for (int k = 0; k < this.bankStaffAccountsArray.Count; k++)
            {
                if (this.bankStaffAccountsArray[k].userid == givenusername)
                {
                    flag = k;
                    break;
                }
            }
            if (this.bankStaffAccountsArray[flag].password == givenpassword) return true;
            else return false;
        }
        public void createUserAccount(string username,string password,double balance)
        {
            string accId=username.Substring(0,3)+ DateTime.Now.Ticks;
            this.bankUserAccountsArray.Add(new UserAccount() { userid=username, password=password,accountBalance=balance,accountid=accId});
            string tempTXNid="TXN"+this.BankId+accId+ DateTime.Now.Ticks+"   +"+balance+"  aval.bal. "+balance;
            this.bankUserAccountsArray[bankUserAccountsArray.Count() - 1].transcations.Add(tempTXNid);
            this.bankUserAccountsArray[bankUserAccountsArray.Count() - 1].amounts.Add(balance);

            Console.WriteLine(tempTXNid);
        }
        public void createStaffAccount(string username, string password)
        {
            this.bankStaffAccountsArray.Add(new BankStaffAccount() { userid = username, password = password });
        }
        public void deleteUserAccount(string givenusername)
        {
            int flag = getIndex(givenusername);
            if (flag == -1)
            {
                Console.WriteLine("Invalid User name");
            }
            else
            {
                this.bankUserAccountsArray.RemoveAt(flag);
            }
        }
        public void updateUserAccount(string oldusername, string newUsername, string newPassword, double newBal)
        {
            int flag = getIndex(oldusername);
            if (flag == -1)
            {
                Console.WriteLine("Invalid User name");
            }
            else
            {
                this.bankUserAccountsArray[flag].userid = newUsername;
                this.bankUserAccountsArray[flag].password = newPassword;
                this.bankUserAccountsArray[flag].accountBalance = newBal;
            }
        }

        public void depositAmountToUserAccount(double Amount, string givenusername)
        {
            int flag = getIndex(givenusername);
            if (flag == -1)
            {
                Console.WriteLine("Invalid User name");
            }
            else
            {
                string accId = this.bankUserAccountsArray[flag].accountid;
                this.bankUserAccountsArray[flag].accountBalance += Amount;
                string tempTXNid = "TXN" + this.BankId + accId + DateTime.Now.Ticks + "   +" + Amount + "  aval.bal. " + this.bankUserAccountsArray[flag].accountBalance;
                this.bankUserAccountsArray[flag].transcations.Add(tempTXNid);
                this.bankUserAccountsArray[flag].amounts.Add(Amount);
            }
        }
        public bool withdrawAmountFromUserAccount(string givenusername, double amount)
        {
            int flag = getIndex(givenusername);
            if (flag == -1)
            {
                Console.WriteLine("Invalid User name");
                return false;
            }
            else
            {
                string accId = this.bankUserAccountsArray[flag].accountid;
                double avalBal = this.bankUserAccountsArray[flag].accountBalance;
                if (amount > avalBal) { Console.WriteLine("Insufficent Funds");return false; }
                else
                {
                    this.bankUserAccountsArray[flag].accountBalance -= amount;
                    string tempTXNid = "TXN" + this.BankId + accId + DateTime.Now.Ticks + "   -" + amount + "  aval.bal. " + this.bankUserAccountsArray[flag].accountBalance;
                    this.bankUserAccountsArray[flag].transcations.Add(tempTXNid);
                    this.bankUserAccountsArray[flag].amounts.Add(amount);
                    Console.WriteLine(amount + " WithDrawn Succesfully");
                    return true;
                }
            }
        }
        public void showTranscationHistory(string username)
        {
            int flag = getIndex(username);
            if (flag == -1)
            {
                Console.WriteLine("Invalid User name");
            }
            else
            {
                int numofTranscations = this.bankUserAccountsArray[flag].transcations.Count();
                for (int i = 0; i < numofTranscations; i++)
                {
                    Console.WriteLine(this.bankUserAccountsArray[flag].transcations[i]);
                }
            }
        }
        public double getAccountBalance(string givenusername)
        {
            int flag = getIndex(givenusername);
            if (flag == -1)
            {
                Console.WriteLine("Invalid User name");
                return 0;
            }
            else
            {
                return this.bankUserAccountsArray[(int)flag].accountBalance;
            }
        }
        public void revertTranction(string txnid, string username) {
            int flag = getIndex(username);
            if (flag == -1)
            {
                Console.WriteLine("Invalid User name");
            }
            else
            {
                
                int numtxns = this.bankUserAccountsArray[flag].transcations.Count();
                int transcationIndex =numtxns;
                for (int i = 0; i < numtxns; i++)
                {
                    if (this.bankUserAccountsArray[flag].transcations[i].Substring(0, 63) == txnid)
                    {
                        transcationIndex = i;
                        break;
                    }
                }
                char plusOrMinus = this.bankUserAccountsArray[flag].transcations[transcationIndex][65];
                double amount = this.bankUserAccountsArray[flag].amounts[transcationIndex];
                if (plusOrMinus == '+')
                {
                    Console.WriteLine("pluss_problem");
                    this.bankUserAccountsArray[flag].accountBalance -= amount;
                }
                else if (plusOrMinus == '-')
                {
                    Console.WriteLine("minus_problem");
                    
                    this.bankUserAccountsArray[flag].accountBalance += amount;
                }
                Console.WriteLine(transcationIndex);
                this.bankUserAccountsArray[flag].transcations.RemoveAt(transcationIndex);
                this.bankUserAccountsArray[flag].amounts.RemoveAt(transcationIndex);
            } 
        }
        public int getIndex(string givenusername)
        {
            int flag = -1;
            for (int k = 0; k < this.bankUserAccountsArray.Count; k++)
            {
                if (this.bankUserAccountsArray[k].userid == givenusername)
                {
                    flag = k;
                    break;
                }
            }
            return flag;
        }
    }*/
    }
}
