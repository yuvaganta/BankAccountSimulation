using BankAccountSimulation.Models;
using BankAccountSimulation.Services;

namespace BankAccountSimulation
{
    class Program
    {
        public static string ReadName()
        {
            string tempname;
            Console.Write("Enter username: ");
            tempname = Console.ReadLine();
            return tempname;
        }
        public static string ReadPassword()
        {
            string tempname;
            Console.Write("Enter Password: ");
            tempname = Console.ReadLine();
            return tempname;
        }
        public static double ReadBal()
        {
            double tempbal;
            Console.Write("Enter balance: ");
            tempbal = Convert.ToDouble(Console.ReadLine());
            return tempbal;
        }
        
        public static void Main(string[] args)
        {
            int loginOption = 0, serviceOptionBankStaff = 0, serviceOptionUser = 0;
            int i, bankSelectionOption = 0;
            string receiverUserName;
            string bankName = "";
            double transferringAmount, debittingAmount, credittingAmount;
            string userName = "", password = "", tempBankName;
            CentralBank centralBank=new CentralBank();
            CentralBankServices centralBankServices=new CentralBankServices(); 
            BankServices bankServices=new BankServices();
            StaffOperations staffOperations= new StaffOperations();
            AccountHolderServices accountHolderServices=new AccountHolderServices();
            int usingBankIndex;
            while (true)
            {
                int loopBreaker = 0;
                Console.WriteLine("Main menu");
                for (i = 0; i < centralBank.banksArray.Count(); i++)
                {
                    try
                    {
                        Console.WriteLine((i + 1) + "." + centralBank.banksArray[i].BankName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No banks registered at the moment\n Create a new bank");
                        break;
                    }

                }
                Console.WriteLine((i + 1) + ". Create New Bank");
                try
                {
                    bankSelectionOption = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Enter a Vaild option ");
                    continue;
                }
                if (bankSelectionOption == i + 1)
                {
                    Console.Write("Enter Bank Name:");
                    tempBankName = Console.ReadLine();
                    centralBankServices.BankCreation(centralBank, tempBankName);
                    centralBankServices.DefaultStaffAccountCreation(centralBank, tempBankName);
                    Console.WriteLine(centralBank.banksArray[centralBank.banksArray.Count() - 1].bankStaffAccountsArray[0].UserId + " " +centralBank.banksArray[centralBank.banksArray.Count() - 1].bankStaffAccountsArray[0].Password);
                    continue;
                }
                else if (0 < bankSelectionOption && bankSelectionOption <= i)
                {
                    usingBankIndex = bankSelectionOption - 1;
                    bankName = centralBank.banksArray[usingBankIndex].BankName;
                }
                else
                {
                    Console.WriteLine("Please enter vaid option");
                    continue;
                }


                Console.WriteLine("------------------------------------------------------");
            
                while (loopBreaker==0)
                {
                    loopBreaker = 1;

                    Console.WriteLine("Login to Account:");
                    Console.WriteLine("1.Account User");
                    Console.WriteLine("2.Bank Staff");
                    try
                    {
                        loginOption = Convert.ToInt32(Console.ReadLine());
                    }

                    catch (Exception e)
                    {

                        loginOption = 0;
                        loopBreaker = 0;
                    }
                    if (loginOption == 1)
                    {
                        while (true)
                        {
                            Console.Write("Enter User name: ");
                            userName = Console.ReadLine();
                            
                            if (!bankServices.CheckForValidUserNameForUser(centralBankServices.GetBank(centralBank,bankName),userName))
                            {
                                Console.WriteLine("Enter vaild username");
                                Console.WriteLine("If You wish to go to login selection please enter 1 else enter any number");
                                int loginSectionConfirmantation = Convert.ToInt32(Console.ReadLine());
                                if (loginSectionConfirmantation == 1)
                                {
                                    loopBreaker = 0;
                                    break;
                                }
                                continue;
                            }
                            Console.Write("Enter password: ");
                            password = Console.ReadLine();
                            if (!bankServices.CheckForValidPasswordForUser(centralBankServices.GetBank(centralBank, bankName), userName, password))
                            {
                                Console.WriteLine("Incorrect Password");
                                Console.WriteLine("If You wish to go to login selection please enter 1 else enter any number");
                                int loginSectionConfirmantation = Convert.ToInt32(Console.ReadLine());
                                if (loginSectionConfirmantation == 1) { loopBreaker = 0; break; }
                                continue;
                            }
                            if (loopBreaker != 0) break;
                        }
                    }
                    else if (loginOption == 2)
                    {
                        while (true)
                        {
                            Console.Write("Enter Staff User name: ");
                            userName = Console.ReadLine();
                            if (!bankServices.CheckForValidUserNameForStaff(centralBankServices.GetBank(centralBank, bankName),userName))
                            {
                                Console.WriteLine("Enter vaild username");
                                Console.WriteLine("If You wish to go to login selection please enter 1 else enter any number");
                                int loginSectionConfirmantation = Convert.ToInt32(Console.ReadLine());
                                if (loginSectionConfirmantation == 1) { loopBreaker = 0; break; }
                                continue;
                            }
                            Console.Write("Enter password: ");
                            password = Console.ReadLine();
                            if (!bankServices.CheckForValidPasswordForStaff(centralBankServices.GetBank(centralBank, bankName),userName, password))
                            {
                                Console.WriteLine("Incorrect Password");
                                Console.WriteLine("If You wish to go to login selection please enter 1 else enter any number");
                                int loginsectionconfirmantation = Convert.ToInt32(Console.ReadLine());
                                if (loginsectionconfirmantation == 1) { loopBreaker = 0; break; }
                                continue;
                            }
                            if(loopBreaker!=0) break;

                        }
                    }
                    else
                    {
                        Console.WriteLine("Enter Vaid Option");
                        loopBreaker = 0;
                    }
                    if (loopBreaker != 0) break;

                }
                if (loginOption == 2)
                {
                    while (true)
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
                            serviceOptionBankStaff = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Enter vaild Service option");
                        }
                        if (serviceOptionBankStaff == 1)
                        {
                            string tempName, tempPassword;
                            double tempBal = 0;
                            tempName = ReadName();
                            tempPassword = ReadPassword();
                            tempBal = ReadBal();
                            
                            staffOperations.CreateUserAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, centralBankServices.GetBank(centralBank, bankName).bankUserAccountHoldersArray,tempName, tempPassword, tempBal);
                            string txnId = "TXN" + centralBankServices.GetBank(centralBank, bankName).BankId + bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName).AccountId+DateTime.Now.Ticks;
                            accountHolderServices.Deposit(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName), bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName), tempBal, txnId,bankName,bankName);
                            continue;
                        }
                        else if (serviceOptionBankStaff == 2)
                        {
                            string tempName;
                            tempName = ReadName();
                            Account account = bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName);
                            AccountHolder accountHolder= bankServices.GetAccountHolder(centralBankServices.GetBank(centralBank, bankName).bankUserAccountHoldersArray,tempName);
                            Console.WriteLine("1.Update Account");
                            Console.WriteLine("2.Delete Account");
                            int updateDelteOption = Convert.ToInt32(Console.ReadLine());
                            if (updateDelteOption == 1)
                            {
                                string updatedUserName, updatedPassword;
                                double updatedBal;
                                Console.WriteLine("Enter the new deatils of " + tempName);
                                updatedUserName = ReadName();
                                updatedPassword = ReadPassword();
                                updatedBal = ReadBal();
                                staffOperations.UpdateUserAccount(account, updatedUserName, updatedPassword,updatedBal);
                                staffOperations.UpdateUserAccountHolder(accountHolder, updatedUserName,updatedPassword, updatedBal);
                                Console.WriteLine("Details Updated");
                            }
                            else if (updateDelteOption == 2)
                            {
                                //BanksArray[usingBankIndex].deleteUserAccount(tempName);
                                staffOperations.DeleteAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, account);
                                staffOperations.DeleteAccountHolder(centralBankServices.GetBank(centralBank, bankName).bankUserAccountHoldersArray, accountHolder);
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("***Enter a vaild option***");
                                continue;
                            }
                        }
                        else if (serviceOptionBankStaff == 3)
                        {
                            string newCurrency;
                            double newExchangeRate;
                            Console.Write("Enter new Accepted Currency : ");
                            newCurrency = Console.ReadLine();
                            Console.Write("Enter Exchange Rate : ");
                            newExchangeRate = Convert.ToDouble(Console.ReadLine());
                            staffOperations.AddNewCurrencyAndExchangeRate(centralBankServices.GetBank(centralBank, bankName), newCurrency, newExchangeRate);
                            
                        }
                        else if (serviceOptionBankStaff == 4)
                        {
                            double newRTGS, newIMPS;
                            Console.WriteLine("present Service Charges for the same bank RTGS: " + centralBankServices.GetBank(centralBank,bankName).RTGSSame + "% IMPS: " + centralBankServices.GetBank(centralBank, bankName).IMPSSame + "%");
                            Console.Write("Enter New RTGS value: ");
                            newRTGS = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Enter New IMPS value: ");
                            newIMPS = Convert.ToDouble(Console.ReadLine());
                            staffOperations.ChangeServicesChargesForSameBank(centralBankServices.GetBank(centralBank,bankName),newRTGS, newIMPS);
                        }
                        else if (serviceOptionBankStaff == 5)
                        {
                            double newRTGS, newIMPS;
                            Console.WriteLine("present Service Charges for the different bank RTGS: " + centralBankServices.GetBank(centralBank, bankName).RTGSDifferent + "% IMPS: " + centralBankServices.GetBank(centralBank, bankName).IMPSDifferent + "%");
                            Console.Write("Enter New RTGS value: ");
                            newRTGS = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Enter New IMPS value: ");
                            newIMPS = Convert.ToDouble(Console.ReadLine()); 
                            staffOperations.ChangeServicesChargesForDifferentBank(centralBankServices.GetBank(centralBank, bankName), newRTGS, newIMPS);

                        }
                        else if (serviceOptionBankStaff == 6) {
                            string tempUserName = ReadName();
                            
                            staffOperations.ShowTranscationHistory(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempUserName));
                            
                        } 
                        else if (serviceOptionBankStaff == 7) {
                            string tempName;
                            tempName = ReadName();
                            string receiverName,receiverBankName;
                            accountHolderServices.ShowTranscationHistory(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName));
                            Console.Write("Enter the Transcationid that needs to be deleted: ");
                            string txnId = Console.ReadLine();
                            if (staffOperations.GetReceiver(bankServices.GetAccount(centralBankServices.GetBank(centralBank,bankName).bankUserAccountsArray,tempName),txnId) == tempName)
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
                            receiverBankName = staffOperations.GetReceiverBank(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, tempName),txnId);
                            staffOperations.RevertTranscation(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray,tempName),bankServices.GetAccount(centralBankServices.GetBank(centralBank,receiverBankName).bankUserAccountsArray,receiverName), txnId);
                        }
                        else if (serviceOptionBankStaff == 8)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine("1.Deposit amount ");
                        Console.WriteLine("2.Withdraw amount ");
                        Console.WriteLine("3.Transfer funds ");
                        Console.WriteLine("4.View transaction history ");
                        Console.WriteLine("5.Main menu");
                        try
                        {
                            serviceOptionUser = Convert.ToInt32(Console.ReadLine());

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Enter vaild Service option");
                        }
                        if (serviceOptionUser == 1)
                        {
                            double tempAmmount;
                            string operatingCurrency;
                            Console.WriteLine("Accepting Currencies:");
                            bankServices.ShowAvailableCurrencies(centralBankServices.GetBank(centralBank,bankName));
                            Console.Write("Enter the Currency: ");
                            operatingCurrency= Console.ReadLine();
                            double exchangeRate = bankServices.GetExchangeRate(centralBankServices.GetBank(centralBank, bankName), operatingCurrency);
                            if (exchangeRate == 0)
                            {
                                Console.WriteLine("Invalid Currency");
                                continue;
                            }
                            Console.Write("Please enter the amount to be deposited "+userName+" : ");
                            tempAmmount = Convert.ToDouble(Console.ReadLine());
                            tempAmmount *= exchangeRate;
                            string txnId = "TXN" + centralBankServices.GetBank(centralBank,bankName).BankId + bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName).AccountId + DateTime.Now.Ticks;
                            accountHolderServices.Deposit(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName), bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName), tempAmmount, txnId,bankName,bankName);
                        }
                        else if (serviceOptionUser == 2) {
                            double amountToWithDraw;
                            Console.Write("Enter amount to WithDraw : ");
                            amountToWithDraw = Convert.ToDouble(Console.ReadLine());
                            string txnId = "TXN" +centralBankServices.GetBank(centralBank,bankName).BankId+ bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName).AccountId + DateTime.Now.Ticks;
                            accountHolderServices.Withdraw(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray,userName), bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName), amountToWithDraw,txnId,bankName,bankName);
                        }
                        else if (serviceOptionUser == 3) {
                            Console.Write("Enter the bank name of receiver: ");
                            double charges=0;
                            string receiverBankName = Console.ReadLine();
                            try
                            {
                                centralBankServices.GetBank(centralBank, receiverBankName);
                            }catch(Exception ex)
                            {
                                Console.WriteLine("***Invalid Bank Name***");
                                continue;
                            }
                            Console.Write("Enter username of receiver: ");
                            receiverUserName = Console.ReadLine();
                            try
                            {
                                bankServices.GetAccount(centralBankServices.GetBank(centralBank,receiverBankName).bankUserAccountsArray,receiverUserName);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Invalid User name"); continue;
                            }
                            Console.Write("Enter Amount to be transffered: ");
                            transferringAmount = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Choose the mode of Transcation:");
                            Console.WriteLine("1.IMPS \n2.RTGS");
                            int modeOfTranscation = Convert.ToInt32(Console.ReadLine());
                            if (modeOfTranscation == 1)
                            {
                                if (bankName == receiverBankName)
                                {
                                    charges=transferringAmount*(centralBankServices.GetBank(centralBank,bankName).IMPSSame)/100;
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
                            else { Console.WriteLine("Enter vaild Option"); }
                            debittingAmount = transferringAmount  ;
                            credittingAmount = transferringAmount-charges;
                            string txnId = "TXN" + centralBankServices.GetBank(centralBank, bankName).BankId + bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName).AccountId + DateTime.Now.Ticks;
                            accountHolderServices.Transfer(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName), bankServices.GetAccount(centralBankServices.GetBank(centralBank, receiverBankName).bankUserAccountsArray, receiverUserName), credittingAmount, debittingAmount, txnId,bankName,receiverBankName);
                        }
                        else if (serviceOptionUser == 4)
                        {
                            
                            accountHolderServices.ShowTranscationHistory(bankServices.GetAccount(centralBankServices.GetBank(centralBank, bankName).bankUserAccountsArray, userName));
                        }
                        else if (serviceOptionUser == 5)
                        {
                            break;
                        }
                    }
                }
                if (serviceOptionBankStaff == 8 || serviceOptionUser == 5)
                {
                    serviceOptionUser = 0;
                    serviceOptionBankStaff = 0;
                    continue;
                }

            }
        }
    }
}