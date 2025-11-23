using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Transaction
    {
        public static void InternalTransfer(Customer user)
        {
            //As long as this is true the loop will continue
            bool transferInProgress = true;

            while (transferInProgress)
            {
                Console.WriteLine("Internal Överföring");

               
                    List<Account> accounts = user.GetAccountList();
                    for (int i = 0; i < accounts.Count; i++)
                    {
                        decimal balance = accounts[i].GetBalance();
                        int accountNumber = accounts[i].GetAccountNumber();
                        Console.WriteLine($"Konto: {i} Har ett saldo av: {balance}");

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
                        decimal ammountToSend = Utilities.GetUserNumber();
                       

                    if(ammountToSend < accounts[accountSender].GetBalance())
                        {
                            accounts[accountSender ].RemoveMoney(ammountToSend);
                            accounts[accountRecipitent].AddMoney(ammountToSend);
                           Console.WriteLine("Uppdaterad konto lista:");
                            for (int i = 0; i < accounts.Count; i++)
                            {
                                decimal balance = accounts[i].GetBalance();
                                int accountNumber = accounts[i].GetAccountNumber();
                                Console.WriteLine($"Konto: {i} Har ett saldo av: {balance}");

                            }
                        transferInProgress = false;
                    }
                    else
                    {
                        Console.WriteLine("Du försöker skicka mer pengar än som finns på kontot");
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
                Console.WriteLine("External Överföring");
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
                        Console.WriteLine($"Konto: {i} Har ett saldo av: {balance}");

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

                        Console.WriteLine("Hur mycket pengar vill du skicka?");
                        int ammountToSend = Utilities.GetUserNumber();

                        // Lite lätt fast, osäker hur jag får mottagarens konton och deras värde :(

                        if(ammountToSend > accounts[accountPicked].GetBalance())
                        {
                            List<User> _users = Bank.GetUsers();
                            foreach (User user1 in _users)
                            {
                                if (user1.GetUsername() == recipient)
                                {
                                    
                                    
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
