using BankAccountSimulation.Contracts;
using BankAccountSimulation.Models;
using BankAccountSimulation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSimulation
{
    internal class ProgramClass
    {
        public static void AvailableBanksReturn(CentralBank centralBank)
        {
            int index = 1;
            foreach (var bank in centralBank.banksArray)
            {
                Console.WriteLine(index + ". " + bank.BankName);
                index++;
            }
        }
        public static void PrintMethod(string message)
        {
            Console.WriteLine(message);
        }
        public static int ReadOption()
        {
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine("Enter Valid Option");
                return 0;
            }
        }
        public static int LoginSection(string bankName,CentralBank centralBank)
        {
            BankServices bankServices= new BankServices();
            CentralBankServices centralBankServices= new CentralBankServices();
            InputReadingHelper inputReadingHelper= new InputReadingHelper();
            string userName, password;
            int flag=0;
            while (true)
            {
                PrintMethod("Login to Account");
                PrintMethod("1.Account User");
                PrintMethod("2.Bank Staff");
                int option=ReadOption();               
                if (option == 0)
                {
                    continue;
                }
                else if (option == 1)
                {
                    userName = inputReadingHelper.ReadName();
                    if(!bankServices.CheckForValidUserNameForUser(centralBankServices.GetBank(centralBank, bankName), userName)){
                        Console.WriteLine("Enter vaild username");
                        continue;
                    }
                    password=inputReadingHelper.ReadPassword();
                    if(!bankServices.CheckForValidPasswordForUser(centralBankServices.GetBank(centralBank, bankName), userName, password))
                    {
                        Console.WriteLine("InValid Password");
                        continue;
                    }
                    flag=UserSection(centralBank,userName,password,bankName);
                    break;
                }
                else
                {
                    userName = inputReadingHelper.ReadName();
                    if (!bankServices.CheckForValidUserNameForStaff(centralBankServices.GetBank(centralBank, bankName), userName))
                    {
                        Console.WriteLine("Enter vaild username");
                        continue;
                    }
                    password = inputReadingHelper.ReadPassword();
                    if (!bankServices.CheckForValidPasswordForStaff(centralBankServices.GetBank(centralBank, bankName), userName, password))
                    {
                        Console.WriteLine("InValid Password");
                        continue;
                    }
                    flag=StaffSection(centralBank,userName,password,bankName);  
                    break;
                }
            }
            return flag;
        }
        public static int StaffSection(CentralBank centralBank,string userName,string password,string bankName)
        {
            int option,flag=0;
            InputReadingHelper inputReadingHelper = new InputReadingHelper();
            CentralBankServices centralBankServices = new CentralBankServices();
            StaffOperations staffOperations = new StaffOperations();
            AccountHolderServices accountHolderServices = new AccountHolderServices();
            BankServices bankServices = new BankServices();
            while (true&&flag==0)
            {
                Console.WriteLine("1.Create new account ");
                Console.WriteLine("2.Update / Delete account ");
                Console.WriteLine("3.Add new Accepted currency ");
                Console.WriteLine("4.Add service charge for same bank account ");
                Console.WriteLine("5.Add service charge for other bank account ");
                Console.WriteLine("6.View account transaction history ");
                Console.WriteLine("7.Revert transaction");
                Console.WriteLine("8.Main menu");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Enter vaild Service option");
                    option = 8;
                }
                switch (option) {
                    case 1:
                        {
                            string tempName, tempPassword;
                            double tempBal = 0;
                            tempName = inputReadingHelper.ReadName();
                            tempPassword = inputReadingHelper.ReadPassword();
                            tempBal = inputReadingHelper.ReadBal();

                            staffOperations.CreateUserAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, centralBankServices.GetBank(centralBank, bankName).bankUserAccountHoldersArray, tempName, tempPassword, tempBal);
                            string txnId = "TXN" + centralBankServices.GetBank(centralBank, bankName).BankId + bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName).AccountId + DateTime.Now.Ticks;
                            accountHolderServices.Deposit(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName), bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName), tempBal, txnId, bankName, bankName);
                            continue;
                        }
                    case 2:
                        {
                            string tempName;
                            tempName = inputReadingHelper.ReadName();
                            Account account = bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName);
                            AccountHolder accountHolder = bankServices.GetAccountHolder(centralBankServices.GetBank(centralBank, bankName).bankUserAccountHoldersArray, tempName);
                            Console.WriteLine("1.Update Account");
                            Console.WriteLine("2.Delete Account");
                            int updateDelteOption = ReadOption();
                            if (updateDelteOption == 1)
                            {
                                string updatedUserName, updatedPassword;
                                double updatedBal;
                                Console.WriteLine("Enter the new deatils of " + tempName);
                                updatedUserName = inputReadingHelper.ReadName();
                                updatedPassword = inputReadingHelper.ReadPassword();
                                updatedBal = inputReadingHelper.ReadBal();
                                staffOperations.UpdateUserAccount(account, updatedUserName, updatedPassword, updatedBal);
                                staffOperations.UpdateUserAccountHolder(accountHolder, updatedUserName, updatedPassword, updatedBal);
                                Console.WriteLine("Details Updated");
                            }
                            else if (updateDelteOption == 2)
                            {
                                staffOperations.DeleteAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, account);
                                staffOperations.DeleteAccountHolder(centralBankServices.GetBank(centralBank, bankName).bankUserAccountHoldersArray, accountHolder);

                            }
                            else
                            {
                                Console.WriteLine("***Enter a vaild option***");

                            }
                            continue;
                        }
                    case 3:
                        {
                            string newCurrency;
                            double newExchangeRate;
                            Console.Write("Enter new Accepted Currency : ");
                            newCurrency = Console.ReadLine();
                            Console.Write("Enter Exchange Rate : ");
                            newExchangeRate = Convert.ToDouble(Console.ReadLine());
                            staffOperations.AddNewCurrencyAndExchangeRate(centralBankServices.GetBank(centralBank, bankName), newCurrency, newExchangeRate);
                            continue;
                        }
                    case 4:
                        {
                            double newRTGS, newIMPS;
                            Console.WriteLine("present Service Charges for the same bank RTGS: " + centralBankServices.GetBank(centralBank, bankName).RTGSSame + "% IMPS: " + centralBankServices.GetBank(centralBank, bankName).IMPSSame + "%");
                            Console.Write("Enter New RTGS value: ");
                            newRTGS = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Enter New IMPS value: ");
                            newIMPS = Convert.ToDouble(Console.ReadLine());
                            staffOperations.ChangeServicesChargesForSameBank(centralBankServices.GetBank(centralBank, bankName), newRTGS, newIMPS);
                            continue;
                        }
                    case 5:
                        {
                            double newRTGS, newIMPS;
                            Console.WriteLine("present Service Charges for the different bank RTGS: " + centralBankServices.GetBank(centralBank, bankName).RTGSDifferent + "% IMPS: " + centralBankServices.GetBank(centralBank, bankName).IMPSDifferent + "%");
                            Console.Write("Enter New RTGS value: ");
                            newRTGS = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Enter New IMPS value: ");
                            newIMPS = Convert.ToDouble(Console.ReadLine());
                            staffOperations.ChangeServicesChargesForDifferentBank(centralBankServices.GetBank(centralBank, bankName), newRTGS, newIMPS);
                            continue;
                        }
                    case 6:
                        {
                            string tempUserName = inputReadingHelper.ReadName();

                            staffOperations.ShowTranscationHistory(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempUserName));
                            continue;
                        }
                    case 7:
                        {
                            string tempName;
                            tempName = inputReadingHelper.ReadName();
                            string receiverName, receiverBankName;
                            accountHolderServices.ShowTranscationHistory(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName));
                            Console.Write("Enter the Transcationid that needs to be deleted: ");
                            string txnId = Console.ReadLine();
                            if (staffOperations.GetReceiver(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName), txnId) == tempName)
                            {
                                Console.WriteLine("Cannot Revert Transcation");
                                continue;
                            }
                            if (staffOperations.GetReceiver(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName), txnId) == "")
                            {
                                Console.WriteLine("Enter valid transcationId");
                                continue;
                            }
                            receiverName = staffOperations.GetReceiver(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName), txnId);
                            receiverBankName = staffOperations.GetReceiverBank(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName), txnId);
                            staffOperations.RevertTranscation(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName), bankServices.GetAccount(centralBankServices.GetBank(centralBank, receiverBankName).bankUserAccountsArray, receiverName), txnId);
                            continue;
                        }
                    case 8:
                        {

                            flag = 1;
                            break;
                        }
                } 
            }
            
            return flag;
        }
        public static int UserSection(CentralBank centralBank, string userName, string password,string bankName)
        {
            AccountHolderServices accountHolderServices=new AccountHolderServices();
            BankServices bankServices=new BankServices();
            CentralBankServices centralBankServices=new CentralBankServices();
            InputReadingHelper inputReadingHelper=new InputReadingHelper();
            int option,flag=0;
            while (true&&flag==0)
            {
                PrintMethod("1.Deposit amount ");
                PrintMethod("2.Withdraw amount ");
                PrintMethod("3.Transfer funds ");
                PrintMethod("4.View transaction history ");
                PrintMethod("5.Main menu");
                option = ReadOption();

                switch (option)
                {
                    case 0:
                        {
                            continue;
                        }
                    case 1:
                        {
                            double tempAmmount;
                            string operatingCurrency;
                            Console.WriteLine("Accepting Currencies:");
                            bankServices.ShowAvailableCurrencies(centralBankServices.GetBank(centralBank, bankName));
                            Console.Write("Enter the Currency: ");
                            operatingCurrency = Console.ReadLine();
                            double exchangeRate = bankServices.GetExchangeRate(centralBankServices.GetBank(centralBank, bankName), operatingCurrency);
                            if (exchangeRate == 0)
                            {
                                Console.WriteLine("Invalid Currency");
                                continue;
                            }
                            Console.Write("Please enter the amount to be deposited " + userName + " : ");
                            tempAmmount = Convert.ToDouble(Console.ReadLine());
                            tempAmmount *= exchangeRate;
                            string txnId = "TXN" + centralBankServices.GetBank(centralBank, bankName).BankId + bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName).AccountId + DateTime.Now.Ticks;
                            accountHolderServices.Deposit(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName), bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName), tempAmmount, txnId, bankName, bankName);
                            break;
                        }
                    case 2:
                        {
                            double amountToWithDraw;
                            Console.Write("Enter amount to WithDraw : ");
                            amountToWithDraw = Convert.ToDouble(Console.ReadLine());
                            string txnId = "TXN" + centralBankServices.GetBank(centralBank, bankName).BankId + bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName).AccountId + DateTime.Now.Ticks;
                            accountHolderServices.Withdraw(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName), bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName), amountToWithDraw, txnId, bankName, bankName);
                            break;
                        }
                        case 3:
                        {
                            string receiverUserName;
                            double transferringAmount, debittingAmount ,credittingAmount;
                            PrintMethod("Enter the bank name of receiver: ");
                            double charges = 0;
                            string receiverBankName = inputReadingHelper.ReadBankName();
                            try
                            {
                                centralBankServices.GetBank(centralBank, receiverBankName);
                            }
                            catch (Exception ex)
                            {
                                PrintMethod(ex.Message);
                                PrintMethod("***Invalid Bank Name***");
                                continue;
                            }
                            PrintMethod("Enter username of receiver: ");
                            receiverUserName = inputReadingHelper.ReadName();
                            try
                            {
                                bankServices.GetAccount(centralBankServices.GetBank(centralBank, receiverBankName).bankUserAccountsArray, receiverUserName);
                            }
                            catch (Exception e)
                            {
                                PrintMethod(e.Message);
                                PrintMethod("Invalid User name"); continue;
                            }
                            PrintMethod("Enter Amount to be transffered: ");
                            transferringAmount = Convert.ToDouble(Console.ReadLine());
                            PrintMethod("Choose the mode of Transcation:");
                            PrintMethod("1.IMPS \n2.RTGS");
                            int modeOfTranscation = ReadOption();
                            if (modeOfTranscation == 1)
                            {
                                if (bankName == receiverBankName)
                                {
                                    charges = transferringAmount * (centralBankServices.GetBank(centralBank, bankName).IMPSSame) / 100;
                                }
                                else
                                {
                                    charges = transferringAmount * (centralBankServices.GetBank(centralBank, bankName).IMPSDifferent) / 100;
                                }
                            }
                            else if (modeOfTranscation == 2)
                            {
                                if (bankName == receiverBankName)
                                {
                                    charges = transferringAmount * (centralBankServices.GetBank(centralBank, bankName).RTGSSame) / 100;
                                }
                                else
                                {
                                    charges = transferringAmount * (centralBankServices.GetBank(centralBank, bankName).RTGSDifferent) / 100;
                                }
                            }
                            else { continue; }
                            debittingAmount = transferringAmount;
                            credittingAmount = transferringAmount - charges;
                            string txnId = "TXN" + centralBankServices.GetBank(centralBank, bankName).BankId + bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName).AccountId + DateTime.Now.Ticks;
                            accountHolderServices.Transfer(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName), bankServices.GetAccount(centralBankServices.GetBank(centralBank, receiverBankName).bankUserAccountsArray, receiverUserName), credittingAmount, debittingAmount, txnId, bankName, receiverBankName);
                            break;
                        }
                    case 4:
                        {
                            accountHolderServices.ShowTranscationHistory(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName));
                            break;
                        }
                        case 5:
                        {
                            flag = 1;
                            continue;  
                        }
                }
            }
            return flag;
        }
        
       public static string BankSelectionSection(CentralBank centralBank)
        {
            ICentralBank centralBankServices=new CentralBankServices();
            InputReadingHelper inputReadingHelper = new InputReadingHelper();
            string bankName;
            while (true)
            {
                AvailableBanksReturn(centralBank);
                PrintMethod((centralBank.banksArray.Count() + 1) + ". Create New Bank");
                int bankOption = ReadOption();
                if (bankOption == 0)
                {
                    continue;
                }
                else if (bankOption == centralBank.banksArray.Count() + 1)
                {
                    bankName = inputReadingHelper.ReadBankName();
                    centralBankServices.BankCreation(centralBank, bankName);
                    centralBankServices.DefaultStaffAccountCreation(centralBank, bankName);
                }
                else
                {
                    bankName = centralBank.banksArray[bankOption - 1].BankName;
                    break;
                }
            }
            return bankName;
        }
        public static void Main(string[] args)
        {
            string bankName;
            int flag;
            CentralBank centralBank = new CentralBank();
           // ICentralBank centralBankServices = new CentralBankServices();
            //IBank bankServices = new BankServices();
           // IStaff staffOperations = new StaffOperations();
            //IAccountHolder accountHolderServices = new AccountHolderServices();
            //InputReadingHelper inputReadingHelper = new InputReadingHelper();
            //PrintingAvailableBanks printingAvailableBanks = new PrintingAvailableBanks();
            while (true)
            {
                bankName = BankSelectionSection(centralBank);
                flag = LoginSection(bankName, centralBank);
                if (flag == 1) continue;
            }
        }
    }
}
