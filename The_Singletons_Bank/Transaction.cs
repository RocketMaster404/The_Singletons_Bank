using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//Kommer nog behöva ändra allting som har med transactions och valutor till decimals tror inte ints kommer fungerar för detta

namespace The_Singletons_Bank
{
    internal class Transaction
    {
        private static Queue<string> _transactionQueue = new Queue<string>();

        
        
        public static void PrintTransactionLogs()
        {
            foreach (var transaction in _transactionQueue)
            {
                Console.WriteLine(transaction);
            }
            Console.ReadKey();
        }
        public static void TransactionLogger(decimal ammountSent, int bankNummer1, string currency1, int bankNummer2)
        {
            string time = DateTime.Now.ToString();
            _transactionQueue.Enqueue($"Account {bankNummer1} sent {ammountSent}{currency1} to {bankNummer2} | Time:{time}");
        }
        public static void InternalTransfer(Customer user)
        {
            //As long as this is true the loop will continue
            bool transferInProgress = true;

            while (transferInProgress)
            {
                Console.WriteLine("Intern Överföring");

               
                    List<Account> accounts = user.GetAccountList();
                    List<SavingAccount> savingAccounts = user.GetSavingAccountList();
                    for (int i = 0; i < accounts.Count; i++)
                    {
                        decimal balance = accounts[i].GetBalance();
                        int accountNumber = accounts[i].GetAccountNumber();
                        Console.WriteLine($"Konto: {i + 1} Har ett saldo av: {balance}{accounts[i].GetCurrency()}");

                    }
                    for (int i = 0; i < savingAccounts.Count; i++)
                    {
                        decimal balance = savingAccounts[i].GetBalance();
                        int accountNumber = savingAccounts[i].GetAccountNumber();
                        Console.WriteLine($"SparKonto: {i + 1+ accounts.Count} Har ett saldo av: {balance}{accounts[i].GetCurrency()}");

                    }
                if (accounts.Count <= 0)
                    {
                        Console.WriteLine("Du behöver skapa ett konto för du har inga just nu");
                        transferInProgress = false;
                        
                    }
                    else
                    {
                        Console.WriteLine("Skriv in numret av kontot du vill skicka pengar från");
                        int accountSender = Utilities.GetUserNumber();

                        Console.WriteLine("Skriv in numret av kontot du vill skicka pengar till?");
                        int accountRecipitent = Utilities.GetUserNumber();

                        Console.WriteLine("Hur mycket will du skicka?");
                        decimal ammountToSend = Utilities.GetUserDecimalInput();
                        decimal ConvertedAmmountToSend = ammountToSend;

                        if (accounts[accountSender - 1].GetCurrency() != null || accounts[accountRecipitent - 1].GetCurrency() != null || accounts[accountSender - 1].GetCurrency() != null || savingAccounts[accountRecipitent - 1].GetCurrency() != null)
                        {
                            ConvertedAmmountToSend = Currency.ConvertCurrency(accounts[accountSender - 1].GetCurrency(), accounts[accountRecipitent - 1].GetCurrency(), Convert.ToDecimal(ammountToSend));
                        }

                    if (ammountToSend <= accounts[accountSender - 1].GetBalance() && ammountToSend >= 0)
                        {
                            accounts[accountSender-1].RemoveMoney(ammountToSend);
                            accounts[accountRecipitent-1].AddMoney(ConvertedAmmountToSend);
                        //Save to log function thingy mahjing
                        TransactionLogger(ammountToSend, accounts[accountSender - 1].GetAccountNumber(), accounts[accountSender - 1].GetCurrency(), accounts[accountRecipitent - 1].GetAccountNumber());
                           
                            Console.WriteLine("Uppdaterad konto lista:");
                            for (int i = 0; i < accounts.Count; i++)
                            {
                                decimal balance = accounts[i].GetBalance();
                                int accountNumber = accounts[i].GetAccountNumber();
                                Console.WriteLine($"Konto: {i + 1} Har ett saldo av: {balance}{accounts[i].GetCurrency()}");

                            }
                        transferInProgress = false;
                    }
                    else
                    {
                        Console.WriteLine("Du kan inte skicka en ogiltig mängd pengar");
                    }

                    

                    }

            }
        }

        public static void ExternalTransfer(Customer user)
        {
            //As long as this is true the loop will continue
            bool transferInProgress = true;

            while (transferInProgress)
            {
                Console.WriteLine("Extern Överföring");
                Console.WriteLine("Skriv in namnet av använderaren du vill skicka pengar till");

                string recipient = Console.ReadLine();
                
                //This will check if the user exist
                if (Bank.userExists(recipient))
                {
                    List<Account> accounts = user.GetAccountList();
                    Console.WriteLine("användare hittad!");
                    for (int i = 0; i < accounts.Count; i++)
                    {
                        decimal balance = accounts[i].GetBalance();
                        int accountNumber = accounts[i].GetAccountNumber();
                        Console.WriteLine($"Konto: {i+1} Har ett saldo av: {balance}");

                    }
                    if (accounts.Count <= 0)
                    {
                        Console.WriteLine("Du behöver skapa ett konto för du har inga just nu");
                        transferInProgress = false;
                    }
                    else
                    {
                        Console.WriteLine("Skriv in numret av kontot du vill skicka pengar från");
                        int accountPicked = Utilities.GetUserNumber();

                        Console.WriteLine("Hur mycket will du skicka?");
                        decimal ammountToSend = Utilities.GetUserDecimalInput();
                        decimal ConvertedAmmountToSend = ammountToSend;

                        //Få detta fungera på något sätt
                        //if (accounts[accountSender - 1].GetCurrency() != null || accounts[accountRecipitent - 1].GetCurrency() != null)
                        //{
                        //    ConvertedAmmountToSend = Currency.ConvertCurrency(accounts[accountSender - 1].GetCurrency(), accounts[accountRecipitent - 1].GetCurrency(), Convert.ToDecimal(ammountToSend));
                        //}
                        // Lite lätt fast, osäker hur jag får mottagarens konton och deras värde :(

                        if (ammountToSend <= accounts[accountPicked].GetBalance() && ammountToSend >= 0)
                        {
                            List<User> _users = Bank.GetUsers(); //User user1 in _users
                            for (int i = 0; i < _users.Count; i++)
                            {                     
                                if (recipient == _users[i-1].GetUsername())
                                {
                                    Console.WriteLine($"Found user: {_users[i-1].GetUsername()} sending the money to him now");

                                    //_users[i].
                                    //Osäker hur man hittar kontots accounts. jag kan hitta dens username men jag kan inte få tillgång till Customer functionen GetAccountList
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Du har inte tillräkligt mycket pengar på kontot för att skicka");
                        }

                        

                    }
                }
                else
                {
                    Console.WriteLine("Does not exist try again");
                }
            }
        }
    }
}
