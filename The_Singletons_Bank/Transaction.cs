using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static The_Singletons_Bank.TransactionHistory;

namespace The_Singletons_Bank
{
    internal class Transaction
    {
        private static Queue<TransactionHistory> _transactionQueue = new Queue<TransactionHistory>();
        //Denna kommer skriva ut alla ens interna översättningar
        public static void PrintInternalTransactions(Customer user)
        {
            Console.Clear();
            Console.WriteLine($"Intern Transaktioner");
            Console.WriteLine($"{"Skickaren",-25} {"Mottagaren",-20} {"Pengarmängd",-10} {"Valuta",-10}  ");
            Console.WriteLine();
            foreach (var transaction in _transactionQueue)
            {
                if(transaction.Type == TransferType.Internal && user == transaction.AccountThatCreatedTheTransaction)
                {
                    Console.WriteLine($"{transaction.senderId,-25} {transaction.recipientId,-20} {transaction.amount,-10} {transaction.recipientCurrency,-10}");
                }
            }
            Console.WriteLine();
        }
        //Denna kommer skriva ut alla ens externa översättningar
        public static void PrintExternalTransactions(Customer user)
        {
            Console.WriteLine($"Extern Transaktioner");
            Console.WriteLine($"{"Skickaren",-25} {"Mottagaren",-20} {"Pengarmängd",-10} {"Valuta",-10}  ");
            Console.WriteLine();
            foreach (var transaction in _transactionQueue)
            {
                if(transaction.Type == TransferType.External && user == transaction.AccountThatCreatedTheTransaction)
                {
                    Console.WriteLine($"{transaction.senderName,-25} {transaction.recipientName,-20} {transaction.amount,-10} {transaction.recipientCurrency,-10}");                   
                }             
            }
        }
        //Detta kommer skriva ut alla ens insättningar
        public static void PrintDeposits(Customer user)
        {
            Console.WriteLine($"Insättningar");
            Console.WriteLine($"{"Konto",-25} {"Valuta",-20} {"Pengarmängd",-10}");
            Console.WriteLine();
            foreach (var transaction in _transactionQueue)
            {
                if(transaction.Type == TransferType.Deposit && user == transaction.AccountThatCreatedTheTransaction)
                {
                    Console.WriteLine($"{transaction.senderId,-25} {transaction.recipientId,-20} {transaction.amount,-10}");                 
                }              
            }
        }
        //Detta kommer hitta vilket namn i konto listan som är kopplad till en int
        public static string returnSpecificAccountName(Customer user, int j)
        {
            List<Account> accounts = user.GetAccountList();
            List<SavingAccount> savingAccounts = user.GetSavingAccountList();

            
            if (j >= 2 && j < accounts.Count + 2)
            {
                return accounts[j - 2].Name;  
            }

            // Check if 'j' is valid for savings accounts list
            if (j >= accounts.Count + 2 && j < accounts.Count + savingAccounts.Count + 2)
            {
                return savingAccounts[j - accounts.Count - 2].Name; 
            }

            
            return "NoName";

        }
        public static void PrintAllTransactionLogs(Customer user)
        {
            PrintInternalTransactions(user);
            Console.WriteLine();
            PrintExternalTransactions(user);
            Utilities.NoContentMsg();
            Console.Clear();
        }

        //Detta kommer skriva ut alla konton en har och information om kontona
        public static void printOutAccounts(Customer user)
        {
            List<Account> accounts = user.GetAccountList();
            List<SavingAccount> savingAccounts = user.GetSavingAccountList();
            for (int i = 0; i < accounts.Count; i++)
            {
                decimal balance = accounts[i].GetBalance();
                int accountNumber = accounts[i].GetAccountNumber();
                string accountName = accounts[i].Name;
                Console.WriteLine($"{i + 2}, {accountName} Har ett saldo av: {balance}{accounts[i].GetCurrency()}");
            }

            for (int i = 0; i < savingAccounts.Count; i++)
            {
                decimal balance = savingAccounts[i].GetBalance();
                int accountNumber = savingAccounts[i].GetAccountNumber();
                string accountName = savingAccounts[i].Name;
                Console.WriteLine($"{i + 2 + accounts.Count}, {accountName} Har ett saldo av: {balance}{savingAccounts[i].GetCurrency()}");
            }
        }
        //Detta kommer skriva ut vad ett specifikt konto beroende på namn har gjort interna och externa överföringar
        public static void PrintOutSpecificAccount(Customer user, string pickedAccountName)
        {
            Console.Clear();
            Console.WriteLine($"Intern Transaktioner");
            Console.WriteLine($"{"Skickaren",-25} {"Mottagaren",-20} {"Pengarmängd",-10} {"Valuta",-10}  ");
            Console.WriteLine();
            foreach (var transaction in _transactionQueue)
            {
                if (transaction.Type == TransferType.Internal && user == transaction.AccountThatCreatedTheTransaction && pickedAccountName == transaction.senderName || transaction.Type == TransferType.Internal && user == transaction.AccountThatCreatedTheTransaction && pickedAccountName == transaction.recipientName)
                {
                    Console.WriteLine($"{transaction.senderId,-25} {transaction.recipientId,-20} {transaction.amount,-10} {transaction.recipientCurrency,-10}");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Extern Transaktioner");
            Console.WriteLine($"{"Skickaren",-25} {"Mottagaren",-20} {"Pengarmängd",-10} {"Valuta",-10}  ");
            Console.WriteLine();
            foreach (var transaction in _transactionQueue)
            {
                if (transaction.Type == TransferType.External && user == transaction.AccountThatCreatedTheTransaction && pickedAccountName == transaction.senderName || transaction.Type == TransferType.External && user == transaction.AccountThatCreatedTheTransaction && pickedAccountName == transaction.recipientName)
                {
                    Console.WriteLine($"{transaction.senderName,-25} {transaction.recipientName,-20} {transaction.amount,-10} {transaction.recipientCurrency,-10}");
                }
            }
        }

        public static void TransactionLogger(Customer user, TransferType transferType, string senderName, string recipientName, int senderId,string senderCurrency, int recipientId, string recipientCurrency, decimal ammountSent)
        {
            string time = DateTime.Now.ToString();
            TransactionHistory transactionToSave = new TransactionHistory(user, transferType, senderName, recipientName, senderId, senderCurrency, recipientId, recipientCurrency, ammountSent, time);
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
                    string accountName = accounts[i].Name;
                    Console.WriteLine($"{i + 1}, {accountName} Har ett saldo av: {balance}{accounts[i].GetCurrency()}");
                }

                for (int i = 0; i < savingAccounts.Count; i++)
                {
                    decimal balance = savingAccounts[i].GetBalance();
                    int accountNumber = savingAccounts[i].GetAccountNumber();
                    string accountName = savingAccounts[i].Name;
                    Console.WriteLine($"{i + 1 + accounts.Count}, {accountName} Har ett saldo av: {balance}{savingAccounts[i].GetCurrency()}");
                }

                //Ifall du inte har ett konto så kommer denna köras och annars så börjar transaktionen
                if (accounts.Count <= 0)
                {
                    Utilities.startColoring(ConsoleColor.Red);
                    Console.WriteLine("Du behöver skapa ett konto för du har inga just nu");
                    Thread.Sleep(2000);
                    Utilities.stopColoring();
                    transferInProgress = false;
                    Console.Clear();

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
                        if (accounts.Count >1)
                        {
                            Console.WriteLine("Skriv in numret av kontot du vill skicka pengar från");
                            accountSender = Utilities.GetUserNumber();
                            if (accountSender <= accounts.Count + savingAccounts.Count && accountSender >= 1)
                            {
                                senderValid = true;
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
                        else
                        {
                            Utilities.startColoring(ConsoleColor.Red);
                            Console.WriteLine("Du måste ha mer än ett konto");
                            Utilities.stopColoring();

                            Thread.Sleep(2000);
                            Console.Clear();
                            transferInProgress = false;
                            validAnswer = true;
                        }
                    }

                    if(transferInProgress == true)
                    {
                        //Detta kommer fråga hur mycket du vill skicka sen ändra hur mycket pengar som läggs till eller tar bort om det är olika valutor
                        Console.WriteLine("Hur mycket will du skicka?");
                        decimal ammountToSend = Utilities.GetUserDecimalInput();
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

                        //Det här kommer lägga till och ta bort pengar från kontona sen spara det i historiken
                        if (canSendMoney)
                        {
                            int sendersAccountNumber = 0;
                            int recipientAccountNumber = 0;
                            string sendersAccountName;
                            string recipientAccountName;

                            if (accountSender < 1 + accounts.Count)
                            {
                                accounts[accountSender - 1].RemoveMoney(ammountToSend);
                                sendersAccountName = accounts[accountSender - 1].Name;
                                sendersAccountNumber = accounts[accountSender - 1].GetAccountNumber();
                            }
                            else
                            {
                                savingAccounts[accountSender - 1 - accounts.Count].RemoveMoney(ammountToSend);
                                sendersAccountName = savingAccounts[accountSender - 1 - accounts.Count].Name;
                                sendersAccountNumber = savingAccounts[accountSender - 1 - accounts.Count].GetAccountNumber();
                            }

                            if (accountRecipitent < 1 + accounts.Count)
                            {
                                accounts[accountRecipitent - 1].AddMoney(ConvertedAmmountToSend);
                                recipientAccountName = accounts[accountRecipitent - 1].Name;
                                recipientAccountNumber = accounts[accountRecipitent - 1].GetAccountNumber();
                            }
                            else
                            {
                                savingAccounts[accountRecipitent - 1 - accounts.Count].AddMoney(ConvertedAmmountToSend);
                                recipientAccountName = savingAccounts[accountRecipitent - 1 - accounts.Count].Name;
                                recipientAccountNumber = savingAccounts[accountRecipitent - 1 - accounts.Count].GetAccountNumber();
                            }

                            TransactionLogger(user, TransferType.Internal, sendersAccountName, recipientAccountName, sendersAccountNumber, sendersCurrency, recipientAccountNumber, recipientCurrency, ConvertedAmmountToSend);

                            Utilities.startColoring(ConsoleColor.Green);
                            Console.WriteLine($"{ConvertedAmmountToSend}{recipientCurrency} har skickat från {sendersAccountName} till {recipientAccountName}");
                            Utilities.stopColoring();
                            Thread.Sleep(2000);
                            Console.Clear();
                            transferInProgress = false;
                        }
                        else
                        {
                            Console.WriteLine("Du kan inte skicka en ogiltig mängd pengar");
                        }
                    }
                }                   
            }
        }

        public static void ExternalTransfer(Customer user)
        {
            //Så länge denna boolen är san så kommer denna loopen fortsätta
            bool transferInProgress = true;
            while (transferInProgress)
            {
                Console.WriteLine("Extern Överföring");
                Console.WriteLine("Skriv in Kontonummret av användaren du vill skicka pengar till");

                string recipient = Console.ReadLine();

                //Detta kommer kolla om användaren finns
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
                        string accountName = accounts[i].Name;
                        Console.WriteLine($"{accountName}: {i + 1} Har ett saldo av: {balance}{accounts[i].GetCurrency()}");
                    }
                    if (accounts.Count <= 0)
                    {
                        Console.WriteLine("Du behöver skapa ett konto först");
                        transferInProgress = false;
                    }
                    else
                    {
                        bool validAnswer = false;
                        int accountPicked = 0;
                        while (!validAnswer)
                        {
                            Console.WriteLine("Skriv in numret av kontot du vill skicka ifrån");
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
                        //Här väljer du hur mycket du vill skicka
                        Console.WriteLine("Hur mycket vill du skicka?");
                        decimal ammountToSend = Utilities.GetUserDecimalInput();
                        decimal ConvertedAmmountToSend = ammountToSend;
                        string currencyType = "SEK";

                        if (ammountToSend <= accounts[accountPicked - 1].GetBalance() && ammountToSend >= 0)
                        {

                            if (recipientAccounts != null && recipientAccounts.Count > 0)
                            {
                                //Detta kommer lägga till och ta bort pengar sen spara det till historiken
                                recipientAccounts[0].AddMoney(ConvertedAmmountToSend);
                                accounts[accountPicked - 1].RemoveMoney(ConvertedAmmountToSend);
                                Console.WriteLine($"Du har skickat {ConvertedAmmountToSend} till {recipient}");
                                Thread.Sleep(2000);
                                Console.Clear();
                                currencyType = recipientAccounts[0].GetCurrency();
                                transferInProgress = false; 
                                TransactionLogger(user, TransferType.External, accounts[accountPicked - 1].Name, customer.GetUsername(), accounts[accountPicked - 1].GetAccountNumber(), currencyType, recipientAccounts[0].GetAccountNumber(), currencyType, ConvertedAmmountToSend);              
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
        public static Queue<TransactionHistory> GetQueue()
        {            
            return _transactionQueue;
        }
    }
}
