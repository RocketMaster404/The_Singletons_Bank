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
        private static Queue<TransactionHistory> _transactionQueue = new Queue<TransactionHistory>();
        public static void PrintInternalTransactions()
        {
            Console.Clear();
            Console.WriteLine($"{"Skickaren",-25} {"Mottagaren",-20} {"Pengarmängd",-10} {"Valuta",-10}  ");
            foreach (var transaction in _transactionQueue)
            {

                Console.WriteLine();
                Console.WriteLine("hi");
                //Console.WriteLine($"{account.Name,-25} {account.GetAccountNumber(),-20} {account.GetBalance(),-10} {account._currency,-10} {"",6}");
                Console.WriteLine($"{transaction.senderId,-25} {transaction.recipientId,-20} {transaction.amount,-10} {transaction.recipientCurrency,-10}");
                Console.WriteLine();

            }
        }
        public static void PrintExternalTransactions()
        {
            Console.WriteLine($"{"Skickaren",-25} {"Mottagaren",-20} {"Pengarmängd",-10} {"Valuta",-10}  ");
            Console.WriteLine();
            foreach (var transaction in _transactionQueue)
            {

                Console.WriteLine($"{transaction.senderId,-25} {transaction.recipientId,-20} {transaction.amount,-10} {transaction.recipientCurrency,-10}");
                Utilities.NoContentMsg();
            }
            Utilities.NoContentMsg();
            Console.Clear();
        }

        public static void PrintTransactionLogs()
        {
            PrintInternalTransactions();
            PrintExternalTransactions();
        }
        public static void TransactionLogger(int transferType, int senderId,string senderCurrency, int recipientId, string recipientCurrency, decimal ammountSent)
        {
            string time = DateTime.Now.ToString();
            TransactionHistory transactionToSave = new TransactionHistory(transferType, senderId, senderCurrency, recipientId, recipientCurrency, ammountSent, time);
            _transactionQueue.Enqueue(transactionToSave);   
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
                    bool senderValid = false;
                    bool recipientValid = false;
                    int accountRecipitent = 0;
                    int accountSender = 0;
                    while (!validAnswer)
                    {
                        Console.WriteLine("Skriv in numret av kontot du vill skicka pengar från");
                        accountSender = Utilities.GetUserNumber();
                        if (accountSender <= accounts.Count + savingAccounts.Count && accountSender >= 1)
                        {
                            senderValid = true;
                        }
                        else
                        {   
                            if(accountSender > accounts.Count)
                            {
                                Console.WriteLine("ogiltigt nummer försök igen!");
                            }
                            else if(accountSender < 0)
                            {
                                Console.WriteLine("Det kan inte vara ett negativt nummer");
                            }else if(accountSender == 0)
                            {
                                Console.WriteLine("Du kan inte välja 0");
                            }
                                
                        }
                        if (senderValid)
                        {
                            Console.WriteLine("Skriv in numret av kontot du vill skicka pengar till?");
                            validAnswer = false;
                            accountRecipitent = Utilities.GetUserNumber();
                            if (accountRecipitent <= accounts.Count + savingAccounts.Count && accountRecipitent >= 1)
                            {
                                recipientValid = true;
                            }
                            else
                            {
                                if (accountSender > accounts.Count)
                                {
                                    Console.WriteLine("ogiltigt nummer försök igen!");
                                }
                                else if (accountSender < 0)
                                {
                                    Console.WriteLine("Det kan inte vara ett negativt nummer");
                                }
                                else if (accountSender == 0)
                                {
                                    Console.WriteLine("Du kan inte välja 0");
                                }
                            }

                            if (senderValid && recipientValid)
                            {
                                if (accountSender != accountRecipitent)
                                {
                                    validAnswer = true;
                                }
                                else
                                {
                                    Utilities.startColoring(ConsoleColor.Red);
                                    Console.WriteLine("Du måste välja olika konton");
                                    Utilities.stopColoring();
                                }
                            }
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

                        TransactionLogger(0, sendersAccountNumber, sendersCurrency, recipientAccountNumber, recipientCurrency,ConvertedAmmountToSend);

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
                            if (accountPicked <= accounts.Count && accountPicked >= 1)
                            {
                                validAnswer = true;
                            }
                            else
                            {
                                if (accountPicked > accounts.Count)
                                {
                                    Console.WriteLine("ogiltigt nummer försök igen!");
                                }
                                else if (accountPicked < 0)
                                {
                                    Console.WriteLine("Det kan inte vara ett negativt nummer");
                                }
                                else if (accountPicked == 0)
                                {
                                    Console.WriteLine("Du kan inte välja 0");
                                }
                            }
                        }


                        Console.WriteLine("Hur mycket will du skicka?");
                        decimal ammountToSend = Utilities.GetUserDecimalInput();
                        decimal ConvertedAmmountToSend = ammountToSend;
                        string currencyType = "Sek";

                        if (ammountToSend <= accounts[accountPicked - 1].GetBalance() && ammountToSend >= 0)
                        {

                            if (recipientAccounts != null && recipientAccounts.Count > 0)
                            {
                                recipientAccounts[0].AddMoney(ConvertedAmmountToSend);
                                accounts[accountPicked - 1].RemoveMoney(ConvertedAmmountToSend);
                                Console.WriteLine($"Du har skickat {ConvertedAmmountToSend} till {recipient}");
                                Thread.Sleep(2000);
                                Console.Clear();
                                currencyType = recipientAccounts[0].GetCurrency();
                                transferInProgress = false;
                                TransactionLogger(1, accounts[accountPicked - 1].GetAccountNumber(), currencyType, recipientAccounts[0].GetAccountNumber(), currencyType, ConvertedAmmountToSend);
                                //TransactionLogger(ConvertedAmmountToSend, accounts[accountPicked - 1].GetAccountNumber(), currencyType, recipientAccounts[0].GetAccountNumber());
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
