using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using The_Singletons_Bank;
using static System.Net.Mime.MediaTypeNames;
using static The_Singletons_Bank.TransactionHistory;

namespace The_Singletons_Bank
{
    internal class Transaction
    {
        private static Queue<TransactionHistory> _transactionQueue = new Queue<TransactionHistory>();

        //Denna metod kommer skriva ut alla ens interna översättningar
        public static void PrintInternalTransactions(Customer user)
        {
            Menu.UnderMenuHeader("--Interna Tansaktioner--");

            Console.WriteLine($"{"Avsändare",-25} {"Mottagare",-20} {"Belopp",-10} {"Valuta",-10}  ");
            Console.WriteLine();
            foreach (var transaction in _transactionQueue)
            {
                if (transaction.Type == TransferType.Internal && user == transaction.AccountThatCreatedTheTransaction)
                {
                    Console.WriteLine($"{transaction.senderName,-25} {transaction.recipientName,-20} {transaction.amount,-10} {transaction.recipientCurrency,-10}");
                }
            }
            Console.WriteLine();
        }

        //Denna metod kommer skriva ut alla ens externa översättningar
        public static void PrintExternalTransactions(Customer user)
        {
            Utilities.startColoring(ConsoleColor.Yellow);
            //Console.WriteLine($"--Extern Transaktioner--");
            Utilities.stopColoring();
            Utilities.DashDivide();
            Console.WriteLine();

            Console.WriteLine($"{"Avsändare",-25} {"Mottagare",-20} {"Belopp",-10} {"Valuta",-10}  ");
            Console.WriteLine();
            foreach (var transaction in _transactionQueue)
            {
                if (transaction.Type == TransferType.External && user == transaction.AccountThatCreatedTheTransaction)
                {
                    Console.WriteLine($"{transaction.senderName,-25} {transaction.recipientId,-20} {transaction.amount,-10} {transaction.recipientCurrency,-10}");
                }
            }
        }

        //Denna metod kommer hitta vilket namn i konto listan som är kopplad till en int
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
            Console.Clear();
            Menu.UnderMenuHeader("--Transaktionshistorik--");
            Console.WriteLine();
            PrintInternalTransactions(user);
            Console.WriteLine();
            PrintExternalTransactions(user);
            Utilities.NoContentMsg();
            Console.Clear();
        }

        //Denna metod kommer skriva ut alla konton en har och information om kontona
        public static void printOutAccounts(Customer user)
        {
            List<Account> accounts = user.GetAccountList();
            List<SavingAccount> savingAccounts = user.GetSavingAccountList();
            for (int i = 0; i < accounts.Count; i++)
            {
                decimal balance = accounts[i].GetBalance();
                int accountNumber = accounts[i].GetAccountNumber();
                string accountName = accounts[i].Name;
                Console.WriteLine($"{i + 2,-3} {accountName,-20} {balance,-15} {accounts[i].GetCurrency(),-10}");
                //Console.WriteLine($"{i + 2}, {accountName} Har ett saldo av: {balance}{accounts[i].GetCurrency()}");
            }

            for (int i = 0; i < savingAccounts.Count; i++)
            {
                decimal balance = savingAccounts[i].GetBalance();
                int accountNumber = savingAccounts[i].GetAccountNumber();
                string accountName = savingAccounts[i].Name;
                Console.WriteLine($"{i + 2 + accounts.Count,-3} {accountName,-20} {balance,-15} {accounts[i].GetCurrency(),-10}");
                //Console.WriteLine($"{i + 2 + accounts.Count}, {accountName} Har ett saldo av: {balance}{savingAccounts[i].GetCurrency()}");
            }
        }

        //Denna metod kommer skriva ut vad ett specifikt konto beroende på namn har gjort interna och externa överföringar
        public static void PrintOutSpecificAccount(Customer user, string pickedAccountName)
        {
            Console.Clear();
            Menu.UnderMenuHeader("--Interna Transaktioner--");
            Console.WriteLine();

            Console.WriteLine($"{"Avsändare",-25} {"Mottagare",-20} {"Belopp",-10} {"Valuta",-10}");
            Console.WriteLine();
            foreach (var transaction in _transactionQueue)
            {
                if (transaction.Type == TransferType.Internal && user == transaction.AccountThatCreatedTheTransaction && pickedAccountName == transaction.senderName || transaction.Type == TransferType.Internal && user == transaction.AccountThatCreatedTheTransaction && pickedAccountName == transaction.recipientName)
                {
                    Console.WriteLine($"{transaction.senderName,-25} {transaction.recipientName,-20} {transaction.amount,-10} {transaction.recipientCurrency,-10}");
                }
            }
            Console.WriteLine();
            Menu.UnderMenuHeader("--Externa Transaktioner--");
            Console.WriteLine();

            Console.WriteLine($"{"Avsändare",-25} {"Mottagare",-20} {"Belopp",-10} {"Valuta",-10}");
            Console.WriteLine();
            foreach (var transaction in _transactionQueue)
            {
                if (transaction.Type == TransferType.External && user == transaction.AccountThatCreatedTheTransaction && pickedAccountName == transaction.senderName || transaction.Type == TransferType.External && user == transaction.AccountThatCreatedTheTransaction && pickedAccountName == transaction.recipientName)
                {
                    Console.WriteLine($"{transaction.senderName,-25} {transaction.recipientId,-20} {transaction.amount,-10} {transaction.recipientCurrency,-10}");
                }
            }
        }

        public static void TransactionLogger(Customer user, TransferType transferType, string senderName, string recipientName, int senderId, string senderCurrency, int recipientId, string recipientCurrency, decimal ammountSent)
        {
            string time = DateTime.Now.ToString();
            TransactionHistory transactionToSave = new TransactionHistory(user, transferType, senderName, recipientName, senderId, senderCurrency, recipientId, recipientCurrency, ammountSent, time);
            _transactionQueue.Enqueue(transactionToSave);
        }

        public static void InternalTransfer(Customer user)
        {
            Console.Clear();
            Menu.UnderMenuHeader("--Intern Överföring--");
            //As long as this is true the loop will continue
            bool transferInProgress = true;

            while (transferInProgress)
            {
                //Detta kommer hämta kontona från användaren
                List<Account> accounts = user.GetAccountList();
                List<SavingAccount> savingAccounts = user.GetSavingAccountList();

                //Detta kommer skriva ut alla konton som användaren har
                //Console.WriteLine($"Intern Transaktioner");
                Console.WriteLine($"{"Dina konton",-25} {"Saldo",-20} {"Valuta",-10}");
                Console.WriteLine();
                for (int i = 0; i < accounts.Count; i++)
                {
                    decimal balance = accounts[i].GetBalance();
                    int accountNumber = accounts[i].GetAccountNumber();
                    string accountName = accounts[i].Name;
                    //Console.WriteLine($"{i + 1}, {accountName} Har ett saldo av: {balance}{accounts[i].GetCurrency()}");
                    Console.WriteLine($"{i + 1}, {accountName,-25} {balance,-17} {accounts[i].GetCurrency(),-10}");

                }

                for (int i = 0; i < savingAccounts.Count; i++)
                {
                    decimal balance = savingAccounts[i].GetBalance();
                    int accountNumber = savingAccounts[i].GetAccountNumber();
                    string accountName = savingAccounts[i].Name;
                    //Console.WriteLine($"{i + 1 + accounts.Count}, {accountName} Har ett saldo av: {balance}{savingAccounts[i].GetCurrency()}");
                    Console.WriteLine($"{i + 1 + accounts.Count}, {accountName,-25} {balance,-17} {accounts[i].GetCurrency(),-10}");
                }

                //Ifall du inte har ett konto så kommer denna köras och annars så börjar transaktionen
                if (accounts.Count <= 0)
                {
                    Utilities.startColoring(ConsoleColor.Red);
                    Console.WriteLine("Du behöver skapa ett konto först!");
                    Thread.Sleep(1500);
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
                        if (accounts.Count > 1)
                        {
                            Utilities.DashDivide();
                            Console.WriteLine("");
                            Console.WriteLine("Skriv in numret av kontot som du vill skicka ifrån");
                            accountSender = Utilities.GetUserNumber();
                            if (accountSender <= accounts.Count + savingAccounts.Count && accountSender >= 1)
                            {
                                if (accountSender < 1 + accounts.Count)
                                {
                                    if (accounts[accountSender - 1].GetBalance()! <= 0)
                                    {
                                        Utilities.startColoring(ConsoleColor.Red);
                                        Console.WriteLine("Det valda kontot har otillräckligt saldo.");
                                        Utilities.stopColoring();
                                    }
                                    else
                                    {
                                        senderValid = true;
                                    }
                                }
                                else
                                {
                                    if (savingAccounts[accountSender - 1 - accounts.Count].GetBalance()! <= 0)
                                    {
                                        Utilities.startColoring(ConsoleColor.Red);
                                        Console.WriteLine("Det valda kontot har otillräckligt saldo.");
                                        Utilities.stopColoring();
                                    }
                                    else
                                    {
                                        senderValid = true;
                                    }
                                }
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
                                else if (accounts[accountSender].GetBalance()! <= 0)
                                {
                                    Console.WriteLine("Det valda kontot har otillräckligt saldo.");
                                }

                            }
                            if (senderValid)
                            {
                                Console.Write("Skriv in nummret för det konto du vill skicka till:");
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
                            Utilities.DashDivide();
                            Utilities.startColoring(ConsoleColor.Red);
                            Console.WriteLine("Du måste ha fler än ett konto");
                            Utilities.stopColoring();

                            Thread.Sleep(1500);
                            Console.Clear();
                            transferInProgress = false;
                            validAnswer = true;
                        }
                    }

                    if (transferInProgress == true)
                    {
                        //Detta kommer fråga hur mycket du vill skicka sen ändra hur mycket pengar som läggs till eller tar bort om det är olika valutor
                        Console.WriteLine("Hur mycket vill du skicka?");
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
                            Account senderAccount;
                            Account recipientAccount;
                            if (accountSender < 1 + accounts.Count)
                            {
                                accounts[accountSender - 1].RemoveMoney(ammountToSend);
                                sendersAccountName = accounts[accountSender - 1].Name;
                                senderAccount = accounts[accountSender - 1];
                                sendersAccountNumber = accounts[accountSender - 1].GetAccountNumber();
                            }
                            else
                            {
                                savingAccounts[accountSender - 1 - accounts.Count].RemoveMoney(ammountToSend);
                                sendersAccountName = savingAccounts[accountSender - 1 - accounts.Count].Name;
                                senderAccount = accounts[accountSender - 1 - accounts.Count];
                                sendersAccountNumber = savingAccounts[accountSender - 1 - accounts.Count].GetAccountNumber();
                            }

                            if (accountRecipitent < 1 + accounts.Count)
                            {
                                accounts[accountRecipitent - 1].AddMoney(ConvertedAmmountToSend);
                                recipientAccountName = accounts[accountRecipitent - 1].Name;
                                recipientAccount = accounts[accountRecipitent - 1];
                                recipientAccountNumber = accounts[accountRecipitent - 1].GetAccountNumber();
                            }
                            else
                            {
                                savingAccounts[accountRecipitent - 1 - accounts.Count].AddMoney(ConvertedAmmountToSend);
                                recipientAccountName = savingAccounts[accountRecipitent - 1 - accounts.Count].Name;
                                recipientAccount = accounts[accountRecipitent - 1 - accounts.Count];
                                recipientAccountNumber = savingAccounts[accountRecipitent - 1 - accounts.Count].GetAccountNumber();
                            }
                            //PendingTransaction pendTransaction = new PendingTransaction(user, TransferType.Internal, sendersAccountName, recipientAccountName, sendersAccountNumber, sendersCurrency, recipientAccountNumber, recipientCurrency, ConvertedAmmountToSend, DateTime.Now);
                            //TransactionQueue.CreateQuedTransaction(user,accountRecipitent); <----------------***
                            TransactionLogger(user, TransferType.Internal, sendersAccountName, recipientAccountName, sendersAccountNumber, sendersCurrency, recipientAccountNumber, recipientCurrency, ConvertedAmmountToSend);

                            Utilities.startColoring(ConsoleColor.Green);
                            Console.WriteLine($"{ConvertedAmmountToSend}{recipientCurrency} har skickat från {sendersAccountName} till {recipientAccountName}");
                            Utilities.stopColoring();
                            Thread.Sleep(1500);
                            Console.Clear();
                            transferInProgress = false;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltig summa!");
                        }
                    }
                }
            }
        }

        private static bool TryFindRecipientAccount(
        int accountNumber,
        out Customer recipientCustomer,
        out Account recipientAccount)
        {
            recipientCustomer = null;
            recipientAccount = null;

            foreach (User u in Bank.GetUsers())
            {
                if (u is not Customer c)
                    continue;
                foreach (Account acc in c.GetAccountList())
                {
                    if (acc.GetAccountNumber() == accountNumber)
                    {
                        recipientCustomer = c;
                        recipientAccount = acc;
                        return true;
                    }
                }
                foreach (SavingAccount sAcc in c.GetSavingAccountList())
                {
                    if (sAcc.GetAccountNumber() == accountNumber)
                    {
                        recipientCustomer = c;
                        recipientAccount = sAcc;
                        return true;
                    }
                }
            }
            return false;
        }

        public static void ExternalTransfer(Customer user)
        {
            Console.Clear();
            Menu.UnderMenuHeader("--Extern Överföring--");
            bool validNumber = true;
            bool True = true;
            while (True)
            {

                Console.WriteLine("Skriv in kontonumret som du vill skicka till:");
                int recipientNumber = Utilities.GetUserNumber();

                if (!TryFindRecipientAccount(recipientNumber, out Customer recipientCustomer, out Account recipientAccount))
                {
                    Utilities.startColoring(ConsoleColor.Red);
                    Console.WriteLine("Mottagarkonto hittades inte.\n");
                    Thread.Sleep(1500);
                    validNumber = false;
                    Utilities.stopColoring();
                    Console.Clear();
                    break;
                }

                if (validNumber == true)
                {
                    Utilities.startColoring(ConsoleColor.Green);
                    Console.WriteLine("Mottagare hittad!");
                    Utilities.stopColoring();
                    Thread.Sleep(1000);

                    Console.Clear();
                    Menu.UnderMenuHeader("--Extern Överföring--");

                    var senderAccounts = user.GetAccountList();

                    if (senderAccounts.Count == 0)
                    {
                        Console.WriteLine("Du har inga konton att skicka pengar från.");
                        return;
                    }

                    Console.WriteLine($"{"#",-3} {"Konto",-20} {"Saldo",-15} {"Valuta",-10}");
                    for (int i = 0; i < senderAccounts.Count; i++)
                    {
                        var acc = senderAccounts[i];
                        Console.WriteLine($"{i + 1,-3} {acc.Name,-20} {acc.GetBalance(),-15} {acc.GetCurrency(),-10}");
                    }

                    int index;
                    while (true)
                    {
                        Console.WriteLine("\nVälj konto att skicka ifrån:");
                        index = Utilities.GetUserNumber() - 1;

                        if (index < 0 || index >= senderAccounts.Count)
                        {
                            Console.WriteLine("Ogiltigt val. Försök igen.");
                            continue;
                        }

                        if (senderAccounts[index].GetBalance() <= 0)
                        {
                            Utilities.startColoring(ConsoleColor.Red);
                            Console.WriteLine("Det valda kontot har otillräckligt saldo.");
                            Utilities.stopColoring();
                            continue;
                        }

                        break;
                    }

                    Account senderAccount = senderAccounts[index];

                    Console.Write("Ange överföringsbelopp:");
                    decimal amount = Utilities.GetUserDecimalInput();

                    if (amount <= 0)
                    {
                        Console.WriteLine("Beloppet måste vara större än 0.");
                        return;
                    }
                    if (amount > senderAccount.GetBalance())
                    {
                        Utilities.startColoring(ConsoleColor.Red);
                        Console.WriteLine("Otillräckligt saldo.");
                        Utilities.stopColoring();
                        return;
                    }

                    senderAccount.RemoveMoney(amount);
                    //recipientAccount.AddMoney(amount);
                    Utilities.startColoring(ConsoleColor.Green);
                    Console.WriteLine($"\nDu har skickat {amount} {senderAccount.GetCurrency()} till konto {recipientAccount.GetAccountNumber()}.");
                    Utilities.stopColoring();
                    Thread.Sleep(1500);
                    Console.Clear();

                    // Spara till historiken
                    TransactionLogger(user, TransferType.External, senderAccount.Name, "none", senderAccount.GetAccountNumber(), senderAccount.GetCurrency(), recipientAccount.GetAccountNumber(), recipientAccount.GetCurrency(), amount);

                    TransactionQueue.CreateQuedTransaction(user, recipientCustomer, senderAccount.GetAccountNumber(), recipientAccount.GetAccountNumber(), amount);

                    return;
                }
            }
        }
        public static Queue<TransactionHistory> GetQueue() { return _transactionQueue; }
    }
}