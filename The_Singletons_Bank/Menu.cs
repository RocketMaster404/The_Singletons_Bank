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

        public static List<string> cases { get; set; }


        public static void PrintLogInMenu()
        {
            Console.WriteLine("Singletons Bank - since 1807\n");
            Console.WriteLine("1. Logga in");
            Console.WriteLine("2. Avsluta");
            Console.Write("Ange val: ");
        }

        public static void LogInMenuChoice()
        {
            bool run = true;
            while (run)
            {
                PrintLogInMenu();
                int input = Utilities.GetUserNumberMinMax(1, 2);

                switch (input)
                {
                    case 1:
                        User? user = null;


                        user = Bank.LogIn();
                        while (user != null)
                        {


                            if (user == null || user.UserIsBlocked)
                            {
                                break;
                            }

                            if (user is Customer customer)
                            {
                                RunProgram.RunCustomerProgram(customer);
                            }
                            else if (user is Admin admin)
                            {
                                RunProgram.RunAdminProgram(admin);
                            }
                        }
                        break;

                    case 2:
                        Console.WriteLine("Programmet avslutas!");
                        run = false;
                        break;
                }
            }
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

        public static void CustomerMainMenuChoice(Customer user)
        {
            int input = Utilities.GetUserNumberMinMax(1, 5);
            switch (input)
            {
                case 1:
                    Customer.ShowCustomerAccounts(user);
                    Customer.ShowCustomerSavingAccounts(user);
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("Överföring");
                    PrintTransferMenu();
                    TransferMenuChoice(user);
                    break;
                case 3:
                    PrintCreateBankAccountMenu();
                    CreateBankAccountMenuChoice(user);
                    break;
                case 4:
                    Loan.ShowLoanMenu(user);
                    break;
                case 5:
                    Console.WriteLine("Loggar ut...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    RunProgram.Run();
                    break;
            }

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

        public static void AdminMainMenuChoice(Admin admin)
        {
            int input = Utilities.GetUserNumberMinMax(1, 5);

            switch (input)
            {
                case 1:
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

                    break;
                case 2:
                    Console.WriteLine("2. Växelkurs");
                    Currency.DisplayExchangeRates();
                    Currency.ChangeCurrencyExchangeRateMenu();
                    break;
                case 3: // I have added this case and functon for unlocking accounts [Simon, 2025-11-19]
                    Console.WriteLine("3. UnBlockAccount");
                    Admin.UnBlockAccount();

                    break;

                case 4:
                    Console.Clear();
                    if (Admin.Loantickets.Count == 0)
                    {
                        Console.WriteLine("Du har inga inkomna ärenden");
                        Utilities.NoContentMsg();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Inkomna ärenden:");
                        PrintLoanHandlingMenu();
                        LoanHandlingMenuChoice();
                        break;
                    }

                case 5:
                    Console.WriteLine("Loggar ut...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    RunProgram.Run();
                    break;
            }


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
                    Console.ReadKey();
                    break;
                case 2:
                    SavingAccount.CreateSavingAccount(user);
                    break;
            }
        }

        public static void PrintLoanHandlingMenu()
        {
            int LoanHandlingCounter = 1;

            cases = new List<string>();

            foreach (KeyValuePair<Customer, decimal> kvp in Admin.Loantickets)
            {
                cases.Add(kvp.Key.GetUsername());
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

        public static void LoanHandlingMenuChoice()
        {
            int choice = Utilities.GetUserNumberMinMax(1, 2);
            if (choice == 1)
            {
                Console.WriteLine("Ange ärende du vill bevilja:");
                int casechoice = Utilities.GetUserNumberMinMax(1, Admin.Loantickets.Count());
                string keyToRemove = cases[(casechoice - 1)];

                foreach (KeyValuePair<Customer, decimal> kvp in Admin.Loantickets)
                {
                    if (keyToRemove == kvp.Key.GetUsername())
                    {
                        Admin.HandleLoanRequest(kvp.Key, kvp.Value);

                        Admin.Loantickets.Remove(kvp.Key);

                        Console.WriteLine($"Förslag skickat till {kvp.Key.GetUsername()}");
                        Utilities.NoContentMsg();
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
                    Account accountSEK = Account.CreateAccount(name, 0, "SEK", user);
                    Console.WriteLine("Konto skapat\n");
                    Account.ShowAccount(accountSEK);
                    break;

                case 2:
                    Console.WriteLine("Ange namnet på ditt konto");
                    name = Console.ReadLine();
                    Account accountUSD = Account.CreateAccount(name, 0, "USD", user);
                    Console.WriteLine("Konto skapat\n");
                    Account.ShowAccount(accountUSD);
                    break;

                case 3:
                    Console.WriteLine("Ange namnet på ditt konto");
                    name = Console.ReadLine();
                    Account accountEUR = Account.CreateAccount(name, 0, "EUR", user);
                    Console.WriteLine("Konto skapat\n");
                    Account.ShowAccount(accountEUR); 
                    break;

            }
        }







    }
}
