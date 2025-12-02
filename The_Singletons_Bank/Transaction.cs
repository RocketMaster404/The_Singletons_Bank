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
            Utilities.NoContentMsg();           
            Console.Clear();
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

                //Detta kommer hämta kontona från användaren
                List<Account> accounts = user.GetAccountList();
                List<SavingAccount> savingAccounts = user.GetSavingAccountList();

                //Detta kommer skriva ut alla konton som användaren har
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
                    Console.WriteLine($"SparKonto: {i + 1 + accounts.Count} Har ett saldo av: {balance}{savingAccounts[i].GetCurrency()}");
                    
                }

                //Ifall du inte har ett konto så kommer denna köras och annars så börjar transaktionen
                if (accounts.Count <= 0)
                {
                    Console.WriteLine("Du behöver skapa ett konto för du har inga just nu");
                    transferInProgress = false;

                }
                else
                {

                    //Här skriver användaren in vilket konto som ska skicka pengar och under det vilket konto som ska få pengar och sen hur mycket som ska skickas

                    bool validAnswer = false;
                    int accountSender = 0;
                    while (!validAnswer)
                    {
                        Console.WriteLine("Skriv in numret av kontot du vill skicka pengar från");
                        accountSender = Utilities.GetUserNumber();
                        if (accountSender <= accounts.Count + savingAccounts.Count && accountSender >= 0)
                        {
                            validAnswer = true;
                        }
                        else
                        {
                            Console.WriteLine("ogiltigt nummer försök igen!");
                        }
                    }


                    Console.WriteLine("Skriv in numret av kontot du vill skicka pengar till?");
                    validAnswer = false;
                    int accountRecipitent = 0;
                    while (!validAnswer)
                    {

                        accountRecipitent = Utilities.GetUserNumber();
                        if (accountRecipitent <= accounts.Count + savingAccounts.Count && accountRecipitent >= 0)
                        {
                            validAnswer = true;
                        }
                        else
                        {
                            Console.WriteLine("ogiltigt nummer försök igen!");
                        }
                    }

                    Console.WriteLine("Hur mycket will du skicka?");
                    decimal ammountToSend = Utilities.GetUserDecimalInput();
                    Console.Clear();
                    decimal ConvertedAmmountToSend = ammountToSend;
                    string sendersCurrency = "TEMP";

                    bool canSendMoney = false;
                    string recipientCurrency = "TEMP";
                    if (accountSender < 1 + accounts.Count)
                    {
                        sendersCurrency = accounts[accountSender - 1].GetCurrency();
                        if (ammountToSend <= accounts[accountSender - 1].GetBalance() && ammountToSend >= 0)
                        {
                            canSendMoney = true;
                        }
                    }
                    else
                    {
                        sendersCurrency = savingAccounts[accountSender - 1 - accounts.Count].GetCurrency();
                        if (ammountToSend <= savingAccounts[accountSender - 1 - accounts.Count].GetBalance() && ammountToSend >= 0)
                        {
                            canSendMoney = true;
                        }
                    }

                    if (accountRecipitent < 1 + accounts.Count)
                    {
                        recipientCurrency = accounts[accountRecipitent - 1].GetCurrency();
                    }
                    else
                    {
                        recipientCurrency = savingAccounts[accountRecipitent - 1 - accounts.Count].GetCurrency();
                    }

                    ConvertedAmmountToSend = Currency.ConvertCurrency(sendersCurrency, recipientCurrency, Convert.ToDecimal(ammountToSend));
                    //}

                    if (canSendMoney)
                    {
                        int sendersAccountNumber = 0;
                        int recipientAccountNumber = 0;

                        if (accountSender < 1 + accounts.Count)
                        {
                            accounts[accountSender - 1].RemoveMoney(ammountToSend);
                            sendersAccountNumber = accounts[accountSender - 1].GetAccountNumber();
                        }
                        else
                        {
                            savingAccounts[accountSender - 1 - accounts.Count].RemoveMoney(ammountToSend);
                            sendersAccountNumber = savingAccounts[accountSender - 1 - accounts.Count].GetAccountNumber();
                        }

                        if (accountRecipitent < 1 + accounts.Count)
                        {
                            accounts[accountRecipitent - 1].AddMoney(ConvertedAmmountToSend);
                            recipientAccountNumber = accounts[accountRecipitent - 1].GetAccountNumber();
                        }
                        else
                        {
                            savingAccounts[accountRecipitent - 1 - accounts.Count].AddMoney(ConvertedAmmountToSend);
                            recipientAccountNumber = savingAccounts[accountRecipitent - 1 - accounts.Count].GetAccountNumber();
                        }

                        TransactionLogger(ammountToSend, sendersAccountNumber, sendersCurrency, recipientAccountNumber);

                        //Detta kommer skriva ut alla konton som användaren har
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
                            Console.WriteLine($"SparKonto: {i + 1 + accounts.Count} Har ett saldo av: {balance}{savingAccounts[i].GetCurrency()}");

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
            //public List<Account> GetAccountList()




            while (transferInProgress)
            {
                Console.WriteLine("Extern Överföring");
                Console.WriteLine("Skriv in namnet av användaren du vill skicka pengar till");

                string recipient = Console.ReadLine();

                //This will check if the user exist
                if (Bank.userExists(recipient))
                {
                    List<Account> accounts = user.GetAccountList();
                    
                    List<User> _users = Bank.GetUsers();
                    Customer customer = Bank.GetSpecificUser(recipient);
                    List<Account> recipientAccounts = customer.GetAccountList();
                    Console.WriteLine("användare hittad!");
                    for (int i = 0; i < accounts.Count; i++)
                    {
                        decimal balance = accounts[i].GetBalance();
                        int accountNumber = accounts[i].GetAccountNumber();
                        Console.WriteLine($"Konto: {i + 1} Har ett saldo av: {balance}{accounts[i].GetCurrency()}");

                    }
                    if (accounts.Count <= 0)
                    {
                        Console.WriteLine("Du behöver skapa ett konto för du har inga just nu");
                        transferInProgress = false;
                    }
                    else
                    {
                        bool validAnswer = false;
                        int accountPicked = 0;
                        while (!validAnswer)
                        {
                            Console.WriteLine("Skriv in numret av kontot du vill skicka pengar från");
                            accountPicked = Utilities.GetUserNumber();
                            if (accountPicked <= accounts.Count && accountPicked >= 0)
                            {
                                validAnswer = true;
                            }
                            else
                            {
                                Console.WriteLine("ogiltigt nummer försök igen!");
                            }
                        }


                        Console.WriteLine("Hur mycket will du skicka?");
                        decimal ammountToSend = Utilities.GetUserDecimalInput();
                        decimal ConvertedAmmountToSend = ammountToSend;

                        if (ammountToSend <= accounts[accountPicked - 1].GetBalance() && ammountToSend >= 0)
                        {

                            if(recipientAccounts != null && recipientAccounts.Count > 0)
                            {
                                recipientAccounts[0].AddMoney(ConvertedAmmountToSend);
                                accounts[accountPicked-1].RemoveMoney(ConvertedAmmountToSend);
                                Console.WriteLine($"Du har skickat {ConvertedAmmountToSend} till {recipient}");
                                Thread.Sleep(2000);
                                Console.Clear();
                                transferInProgress = false;
                            }
                            else
                            {
                                Console.WriteLine("Personen du försöker skicka till har inget konto");
                                transferInProgress = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Du har inte tillräkligt mycket pengar på kontot för att skicka");
                            transferInProgress = false;
                        }



                    }
                }
                else
                {
                    Console.WriteLine("Användaren hittades inte, försök igen");
                }
            }
        }
    }
}
