using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Menu
    {


        public static void PrintLogInMenu()
        {
            Console.WriteLine("Singletons Bank - since 1807\n");
            Console.WriteLine("1. Logga in");
            Console.WriteLine("2. Avsluta");
            Console.Write("Ange val: ");
        }



        public static void PrintCustomerMainMenu()
        {
            Console.WriteLine("Meny");
            Console.WriteLine("1. Kontoöversikt"); // Undermeny (Transaktions historik)
            Console.WriteLine("2. Överföring"); // gör undermeny
            Console.WriteLine("3. Skapa konto"); // Gör undermeny
            Console.WriteLine("4. Lån"); // gör under meny
            Console.WriteLine("5. Logga ut");
            Console.Write("Ange val: ");
        }

        public static bool CustomerMainMenuChoice(Customer user)
        {
            int input = Utilities.GetUserNumberMinMax(1, 5);
            switch (input)
            {
                case 1:
                    Console.Clear();
                    Customer.ShowCustomerAccounts(user);
                    Customer.ShowCustomerSavingAccounts(user);
                    Utilities.NoContentMsg();
                    return true;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Överföring");
                    PrintTransferMenu();
                    TransferMenuChoice(user);
                    return true;
                case 3:
                    PrintCreateBankAccountMenu();
                    CreateBankAccountMenuChoice(user);
                    return true;
                case 4:
                    ShowLoanMenu(user);
                    return true;
                case 5:
                    Console.WriteLine("Loggar ut...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    return false;

            }
            return true;
        }

        public static void PrintAdminMainMenu()
        {
            Console.WriteLine("Admin Meny\n");
            Console.WriteLine("1. Skapa användare");
            Console.WriteLine("2. Växelkurs");
            Console.WriteLine("3. UnBlockAccount"); // I have added this choise [Simon, 2025-11-19]
            Console.WriteLine("4. Hantera låneförfrågan");// I have added this choise [Daniel, 2025-11-25]
            Console.WriteLine("5. Logga ut");
        }

        public static bool AdminMainMenuChoice(Admin admin)
        {
            int input = Utilities.GetUserNumberMinMax(1, 5);

            switch (input)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("1. Skapa användare");
                    Console.WriteLine("2. Skapa Admin");
                    int choice = Utilities.GetUserNumberMinMax(1, 2);
                    if (choice == 1)
                    {
                        Bank.AddCustomer();
                    }
                    else if (choice == 2)
                    {
                        Bank.AddAdminAccount();
                    }

                    return true;
                case 2:
                    Console.Clear();
                    Console.WriteLine("2. Växelkurs");
                    Currency.DisplayExchangeRates();
                    bool changeCurrency = Currency.AskIfToChangeCurrencyExchangeRate();
                    if (changeCurrency)
                    {
                        Currency.ChangeCurrencyExchangeRateMenu();
                    }

                    return true;
                case 3: // I have added this case and functon for unlocking accounts [Simon, 2025-11-19]
                    Console.Clear();
                    Console.WriteLine("3. UnBlockAccount");
                    Admin.UnBlockAccount();

                    return true;

                case 4:
                    Console.Clear();
                    if (Admin.Loantickets.Count == 0)
                    {
                        Console.WriteLine("Du har inga inkomna ärenden");
                        Utilities.NoContentMsg();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Inkomna ärenden:");
                        PrintAdminLoanHandlingMenu();
                        AdminLoanHandlingMenuChoice();
                        return true;
                    }

                case 5:
                    Console.WriteLine("Loggar ut...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    return false;
            }
            return true;

        }

        public static void PrintTransferMenu()
        {
            Console.WriteLine("1. Internal Överföring");
            Console.WriteLine("2. External Överföring");
            Console.WriteLine("3. Se historiken");
        }
        public static void TransferMenuChoice(Customer user)
        {
            int input = Utilities.GetUserNumberMinMax(1, 3);
            switch (input)
            {
                case 1:
                    Transaction.InternalTransfer(user);
                    break;
                case 2:
                    Transaction.ExternalTransfer(user);
                    break;
                case 3:
                    Transaction.PrintTransactionLogs();
                    break;
            }
        }

        public static void PrintCreateBankAccountMenu()
        {
            Console.WriteLine("1. Skapa konto");
            Console.WriteLine("2. Skapa sparkonto");
        }
        public static void CreateBankAccountMenuChoice(Customer user)
        {

            int input = Utilities.GetUserNumberMinMax(1, 2);
            switch (input)
            {
                case 1:
                    PrintCreateCurrencyAccountMenu();
                    CreateCurrencyAccountChoice(user);
                    break;
                case 2:
                    SavingAccount.CreateSavingAccount(user);
                    break;
            }
        }

        public static void PrintAdminLoanHandlingMenu()
        {
            int LoanHandlingCounter = 1;

            Admin.cases = new List<string>();

            foreach (KeyValuePair<Customer, decimal> kvp in Admin.Loantickets)
            {
                Admin.cases.Add(kvp.Key.GetUsername());
                Utilities.DashDivide();
                Console.WriteLine(LoanHandlingCounter + ".");
                Console.WriteLine($"Förfrågan inkommen från: {kvp.Key.GetUsername()}");
                //Lägg till kredittrovärdighet?
                Console.WriteLine($"Ansökt belopp:{kvp.Value}SEK");
                Utilities.DashDivide();
                LoanHandlingCounter++;
            }
            Console.WriteLine("\nVad vill du göra?\n");
            Console.WriteLine("1.Hantera lån");
            Console.WriteLine("2.Gå tillbaka");
        }

        public static void AdminLoanHandlingMenuChoice()
        {
            int choice = Utilities.GetUserNumberMinMax(1, 2);
            if (choice == 1)
            {
                Console.WriteLine("Ange ärende du vill hantera:");
                int casechoice = Utilities.GetUserNumberMinMax(1, Admin.Loantickets.Count());
                string keyToRemove = Admin.cases[(casechoice - 1)];//Key to remove blir username för den som skickade låneförslag. 

                foreach (KeyValuePair<Customer, decimal> kvp in Admin.Loantickets)//Loopar för att hitta rätt användare med keytoremove
                {
                    if (keyToRemove == kvp.Key.GetUsername())//om användaren finns i dictionaryn så hanterar man det caset
                    {
                        Utilities.DashDivide();
                        Console.WriteLine("1.Godkänn låneansökan\n2.Avslå låneansökan");
                        int handlingchoice = Utilities.GetUserNumberMinMax(1, 2);

                        if (handlingchoice == 1)
                        {
                            Admin.HandleLoanRequest(kvp.Key, kvp.Value);

                            Admin.Loantickets.Remove(kvp.Key);

                            Console.WriteLine($"Förslag skickat till {kvp.Key.GetUsername()}");
                            Utilities.NoContentMsg();
                        }
                        else
                        {
                            Console.WriteLine("Du har nekat låneförfrågan.\nVar god skriv ett meddelande till kund (valfritt):");
                            string msg = Console.ReadLine() + $"\n\nInfo: Låneförfrågan gällande {kvp.Value}SEK avslås.";

                            Customer owner = kvp.Key;
                            Admin.Sendinvoice(owner, msg);
                            Console.WriteLine("Meddelande skickat.");

                            Admin.Loantickets.Remove(kvp.Key);
                            Utilities.NoContentMsg();
                        }
                    }
                }
            }
            else
            {
                Console.Clear();
            }
        }

        public static void PrintCreateCurrencyAccountMenu()
        {
            Console.WriteLine("Skapa konto");
            Console.WriteLine("1. Konto i SEK");
            Console.WriteLine("2. Konto i USD");
            Console.WriteLine("3. Konto i EUR");
            Console.Write("Ange val: ");
        }

        public static void CreateCurrencyAccountChoice(Customer user)
        {
            int input = Utilities.GetUserNumberMinMax(1, 3);
            string name;

            switch (input)
            {
                case 1:
                    Console.WriteLine("Ange namnet på ditt konto");
                    name = Console.ReadLine();
                    Account accountSEK = Account.CreateAccount(name, 1000, "SEK", user);
                    Console.WriteLine("Konto skapat\n");
                    Account.ShowAccount(accountSEK);
                    break;

                case 2:
                    Console.WriteLine("Ange namnet på ditt konto");
                    name = Console.ReadLine();
                    Account accountUSD = Account.CreateAccount(name, 10, "USD", user);
                    Console.WriteLine("Konto skapat\n");
                    Account.ShowAccount(accountUSD);
                    break;

                case 3:
                    Console.WriteLine("Ange namnet på ditt konto");
                    name = Console.ReadLine();
                    Account accountEUR = Account.CreateAccount(name, 10, "EUR", user);
                    Console.WriteLine("Konto skapat\n");
                    Account.ShowAccount(accountEUR);
                    break;

            }
            Utilities.NoContentMsg();
        }

        public static void ShowLoanMenu(Customer owner)
        {
            Console.Clear();
            Console.WriteLine("1.Visa mina lån");
            Console.WriteLine("2.Mina ärenden");
            Console.WriteLine("3.Ta nytt lån");
            Console.WriteLine("4.Gå tillbaka");
            int choice = Utilities.GetUserNumberMinMax(1, 4);

            switch (choice)
            {
                case 1:
                    if (owner._loans.Count == 0)
                    {
                        Console.WriteLine("\nDu har inga lån.");
                        Utilities.NoContentMsg();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Mina lån\n");
                        foreach (Loan loan in owner._loans)
                        {
                            Utilities.DashDivide();
                            Console.WriteLine($"Lån: {loan.Loanamount}Kr\nRäntesats: {loan.ShowLoanInterestrate()}%\nLånekostnad: {(loan.ShowLoanInterestrate() / 100) * loan.Loanamount}Kr ");
                            Utilities.DashDivide();
                        }
                        Utilities.NoContentMsg();
                    }
                    break;

                case 2:
                    if (owner._inbox.Count() == 0 && !Admin.Loantickets.ContainsKey(owner))
                    {
                        Console.WriteLine("\nDu har inga ärenden att hantera just nu.");
                        Utilities.NoContentMsg();
                    }
                    else if (Admin.Loantickets.ContainsKey(owner))
                    {
                        Console.Clear();
                        Console.WriteLine("Ditt ärende hanteras just nu av banken. Återkom vid ett senare tillfälle.");
                        Utilities.NoContentMsg();
                    }
                    else if (!owner._inbox.Contains("Ränta:"))
                    {
                        Console.WriteLine("Du har ett nytt meddelande angående din låneansökan:");
                        owner.ShowInbox();
                        Console.WriteLine("\nMeddelande raderas när du återgår till menyn");
                        owner._inbox.Clear();
                        Utilities.NoContentMsg();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Dina ärenden:\n");
                        owner.ShowInbox();
                        Console.WriteLine("\n Vad vill du göra?\n");
                        Console.WriteLine("1.Hantera ärende");
                        Console.WriteLine("2.Gå tillbaka");

                        int userchoice = Utilities.GetUserNumberMinMax(1, 2);
                        if (userchoice == 1)
                        {
                            bool accept = owner.HandleLoanSuggestion(1, owner);//Satte siffran 1 då användaren inte kan ha fler än 1 lån åt gången just nu. [Daniel-01/12}
                            break;
                        }
                        else
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine("Ange önskat lånebelopp:");
                    Loan.CreateLoan(owner);
                    break;

                default:
                    Console.Clear();
                    break;
            }

        }





    }
}
