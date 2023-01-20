using System;

namespace BankAccountSimulation
{
    internal class InputReadingHelper
    {
        public  string ReadName()
        {
            
            Console.Write("Enter username: ");
            try
            {
                return Console.ReadLine();
            }catch(Exception ex)
            {
                Console.WriteLine("Enter valid Input");
                return null;
            }
        }
        public string ReadPassword()
        {
            
            Console.Write("Enter Password: ");
            try
            {
                return Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Enter valid Input");
                return null;
            }
        }
        public  double ReadBal()
        {
            Console.Write("Enter balance: ");
            try
            {
                return Convert.ToDouble(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Enter valid Input");
                return 0;
            }
        }
        public string ReadBankName()
        {
            Console.Write("Enter Name of the bank: ");
            try
            {
                return Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Enter valid Input");
                return "";
            }
        }
    }
}
