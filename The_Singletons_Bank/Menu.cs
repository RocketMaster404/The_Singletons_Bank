using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Menu
    {


        public static void PrintLogInMenu()
        {
            Console.WriteLine("Singletons Bank - since 1807\n");
            Console.WriteLine($"1. Logga in",-25);
            Console.WriteLine($"2. Avsluta",-25);
            Console.Write($"\nAnge val: ",-25);
        }



        public static void PrintCustomerMainMenu()
        {

            Console.WriteLine("Meny");
            UnderMenuHeader("--Huvudmeny--");
            Console.WriteLine("1. Kontoöversikt"); // Undermeny (Transaktions historik)
            Console.WriteLine("2. Överföring"); // gör undermeny
            Console.WriteLine("3. Skapa konto"); // Gör undermeny
            Console.WriteLine("4. Lån"); // gör under meny
            Console.WriteLine("5. Insättning");
            Console.WriteLine("6. Logga ut");
            Console.Write("\nAnge val: ");
        }

        public static bool CustomerMainMenuChoice(Customer user)
        {
            int input = Utilities.GetUserNumberMinMax(1, 6);
            switch (input)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine($"{"Konto",-25} {"Kontonummer",-20} {"Saldo",-10} {"Valuta",-10}  ");
                    Console.WriteLine();
                    Customer.ShowCustomerAccounts(user);
                    Console.WriteLine();
                    //Utilities.DashDivide();
                    Console.WriteLine($"{"Sparkonto",-25} {"Kontonummer",-20} {"Saldo",-10} {"Valuta",-10} {"Ränta",4} ");
                    Console.WriteLine();
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
                    Console.Clear();
                    PrintCreateBankAccountMenu();
                    CreateBankAccountMenuChoice(user);
                    return true;
                case 4:
                    ShowLoanMenu(user);
                    return true;
                case 5:
                    Console.Clear();
                    DepositMenu(user);
                    Console.Clear();
                    return true;
                case 6:
                    Console.WriteLine("Loggar ut...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    return false;

            }
            return true;
        }

        public static void PrintAdminMainMenu()
        {
            UnderMenuHeader("--Admin Huvudmeny--");
            //Console.WriteLine("Admin Meny\n");
            Console.WriteLine("1. Skapa användare");
            Console.WriteLine("2. Växelkurs");
            Console.WriteLine("3. Avblockera Användare"); // I have added this choise [Simon, 2025-11-19]
            if (Admin.Loantickets.Count > 0)
            {
                Console.WriteLine($"4. Hantera låneförfrågan[{Admin.Loantickets.Count}]");
            }
            else
            {

                Console.WriteLine("4. Hantera låneförfrågan");
            }
            Console.WriteLine("5. Logga ut");
        }
        public static bool AskIfToChangeCurrencyExchangeRate()
        {
            bool answer = false;
            Console.WriteLine("Vill du ändra någon av valutornas växelkurs?");

            Utilities.startColoring(ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine("[1]: Ja");
            Utilities.stopColoring();

            Utilities.startColoring(ConsoleColor.Red, ConsoleColor.Black);
            Console.WriteLine("[2]: Nej");
            Utilities.stopColoring();

            int stringAnswer = Utilities.GetUserNumberMinMax(1, 2);
            if (stringAnswer == 1)
            {
                answer = true;
            }
            else
            {
                answer = false;
                Console.Clear();
            }
            return answer;
        }

        public static bool AdminMainMenuChoice(Admin admin)
        {
            int input = Utilities.GetUserNumberMinMax(1, 5);

            switch (input)
            {
                case 1:
                    UnderMenuHeader("--Skapa ny användare--");
                    Console.WriteLine("1. Skapa Kund");
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
                    UnderMenuHeader("--Valutakurser--");
                    Currency.DisplayExchangeRates();
                    bool changeCurrency = AskIfToChangeCurrencyExchangeRate();
                    if (changeCurrency)
                    {
                        Currency.ChangeCurrencyExchangeRateMenu();
                    }

                    return true;
                case 3: // I have added this case and functon for unlocking accounts [Simon, 2025-11-19]
                    UnderMenuHeader("--Avblockera användare--");
                    Admin.AvBlockeraAnvändare();

                    return true;

                case 4:
                    UnderMenuHeader("--Inkomna låneförfrågningar--");
                    if (Admin.Loantickets.Count == 0)
                    {
                        Console.WriteLine("Du har inga inkomna ärenden");
                        Utilities.NoContentMsg();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Nya ärenden:");
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
            UnderMenuHeader("--Överföringsmeny--");
            Console.WriteLine("1. Intern Överföring");
            Console.WriteLine("2. Extern Överföring");
            Console.WriteLine("3. Se historiken");
            Console.WriteLine("4. Gå tillbaka");
        }
        public static void TransferMenuChoice(Customer user)
        {
            int input = Utilities.GetUserNumberMinMax(1, 4);
            switch (input)
            {
                case 1:
                    Transaction.InternalTransfer(user);
                    break;
                case 2:
                    Transaction.ExternalTransfer(user);
                    break;
                case 3:
                    PrintAccountHistoryPicker(user);

                    break;
                case 4:
                    Console.Clear();
                    break;
            }
        }

        public static void PrintAccountHistoryPicker(Customer user)
        {
            Console.Clear();
            Utilities.startColoring(ConsoleColor.Yellow);
            Console.WriteLine($"--Transaktion Historik--");
            Utilities.stopColoring();
            Utilities.DashDivide();
            Console.WriteLine("");
            Console.WriteLine($"{"#",-3} {"Konto",-20} {"Saldo",-15} {"Valuta",-10}");
            Console.WriteLine("");
            Console.WriteLine("1   Alla konton");
            Transaction.printOutAccounts(user);
            PrintAccountHistoryOutput(user);
        }
        public static void PrintAccountHistoryOutput(Customer user)
        {
            int input = Utilities.GetUserNumberMinMax(1, user.GetAccountList().Count + user.GetSavingAccountList().Count + 1);
            if (input == 1)
            {
                Transaction.PrintAllTransactionLogs(user);
            }
            else
            {
                string answerName = Transaction.returnSpecificAccountName(user, input);
                Transaction.PrintOutSpecificAccount(user, answerName);
                Utilities.DashDivide();
                Console.ReadKey();
            }
            Console.Clear();
        }

        public static void PrintCreateBankAccountMenu()
        {
            UnderMenuHeader("--Öppna konto--");
            Console.WriteLine("1. Skapa konto");
            Console.WriteLine("2. Skapa sparkonto");
            Console.WriteLine("3. Gå tillbaka");
        }
        public static void CreateBankAccountMenuChoice(Customer user)
        {

            int input = Utilities.GetUserNumberMinMax(1, 3);
            switch (input)
            {
                case 1:
                    PrintCreateCurrencyAccountMenu();
                    CreateCurrencyAccountChoice(user);
                    DatabaseAccounts.AddAllAccounts(Bank.GetUsers());
                    DatabaseAccounts.LoadAllAccounts(Bank.GetUsers());
                    break;
                case 2:
                    SavingAccount.CreateSavingAccount(user);
                    DatabaseAccounts.AddAllAccounts(Bank.GetUsers());
                    DatabaseAccounts.LoadAllAccounts(Bank.GetUsers());
                    break;
                case 3:
                    Console.Clear();
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
                Console.Write("Kredittrovärdighet: ");
                Console.WriteLine(kvp.Key.CredibilityCalculator());
                Utilities.stopColoring();
                Console.WriteLine($"Ansökt belopp: {kvp.Value} SEK");
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
                Console.Write("Ange ärendenummer från listan:");
                int casechoice = Utilities.GetUserNumberMinMax(1, Admin.Loantickets.Count());
                string keyToRemove = Admin.cases[(casechoice - 1)];//Key to remove blir username för den som skickade låneförslag. 

                foreach (KeyValuePair<Customer, decimal> kvp in Admin.Loantickets)//Loopar för att hitta rätt användare med keytoremove
                {
                    if (keyToRemove == kvp.Key.GetUsername())//om användaren finns i dictionaryn så hanterar man det caset
                    {
                        Console.Clear();
                        UnderMenuHeader("--Inkomna låneförfrågningar--");
                        Utilities.DashDivide();
                        Console.WriteLine($"Förfrågan inkommen från: {kvp.Key.GetUsername()}");
                        Console.Write("Kredittrovärdighet: ");
                        Console.WriteLine(kvp.Key.CredibilityCalculator());
                        Utilities.stopColoring();
                        Console.WriteLine($"Ansökt belopp: {kvp.Value} SEK");
                        Utilities.DashDivide();
                        Console.WriteLine("\n1.Godkänn låneansökan\n2.Avslå låneansökan");
                        Console.Write("");
                        int handlingchoice = Utilities.GetUserNumberMinMax(1, 2);

                        if (handlingchoice == 1)
                        {
                            Admin.HandleLoanRequest(kvp.Key, kvp.Value);

                            Admin.Loantickets.Remove(kvp.Key);

                            Console.WriteLine($"\nFörslag skickat till {kvp.Key.GetUsername()}");
                            Utilities.NoContentMsg();
                        }
                        else
                        {
                            Console.WriteLine("Du har nekat låneförfrågan.\nVar god skriv ett meddelande till kund (valfritt):");
                            string msg = $"Meddelande från bank: Låneförfrågan gällande {kvp.Value}SEK avslås.\n\n" + " - " + Console.ReadLine() + "\n Mvh Singletons bank";

                            Customer owner = kvp.Key;
                            Admin.Sendinvoice(owner, msg);
                            Console.WriteLine("\nMeddelande skickat.");

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
                    Console.WriteLine($"{"Kontonamn",-25} {"Kontonummer",-20} {"Saldo",-10} {"Valuta",-10} {"",6}");
                    Account.ShowAccount(accountSEK);
                    break;

                case 2:
                    Console.WriteLine("Ange namnet på ditt konto");
                    name = Console.ReadLine();
                    Account accountUSD = Account.CreateAccount(name, 10, "USD", user);
                    Console.WriteLine("Konto skapat\n");
                    Console.WriteLine($"{"Kontonamn",-25} {"Kontonummer",-20} {"Saldo",-10} {"Valuta",-10} {"",6}");
                    Account.ShowAccount(accountUSD);
                    break;

                case 3:
                    Console.WriteLine("Ange namnet på ditt konto");
                    name = Console.ReadLine();
                    Account accountEUR = Account.CreateAccount(name, 10, "EUR", user);
                    Console.WriteLine("Konto skapat\n");
                    Console.WriteLine($"{"Kontonamn",-25} {"Kontonummer",-20} {"Saldo",-10} {"Valuta",-10} {"",6}");
                    Account.ShowAccount(accountEUR);
                    break;

            }
            Utilities.NoContentMsg();
        }

        public static void ShowLoanMenu(Customer owner)
        {
            UnderMenuHeader("--Lånemeny--");
            Console.WriteLine("1.Visa mina lån");
            Console.WriteLine("2.Ta nytt lån");
            if (owner._inbox.Count > 0)
            {
                Console.WriteLine($"3.Mina ärenden[{owner._inbox.Count}]");
            }
            else
            {
                Console.WriteLine("3.Mina ärenden");
            }

            Console.WriteLine("4.Gör avbetalning");
            Console.WriteLine("5.Gå tillbaka");
            int choice = Utilities.GetUserNumberMinMax(1, 5);


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
                        UnderMenuHeader("--Lånemeny--");
                        Console.WriteLine("Mina lån\n");
                        int counter = 1;
                        foreach (Loan loan in owner._loans)
                        {
                            Utilities.DashDivide();
                            Console.WriteLine(counter + ".");
                            Console.WriteLine($"Lån: {loan.Loanamount}Kr\nRäntesats: {loan.ShowLoanInterestrate()}%\nÅrlig räntekostnad: {(loan.ShowLoanInterestrate() / 100) * loan.Loanamount}Kr ");
                            Utilities.DashDivide();
                            counter++;
                        }
                        Utilities.NoContentMsg();
                    }
                    break;

                case 2:
                    UnderMenuHeader("--Lånemeny--");
                    Console.WriteLine("-- Ansök om nytt lån --");
                    Console.Write($"Ditt maximala lånebelopp är {owner.TotalFunds() * 5} SEK\nAnge önskat lånebelopp:");
                    Loan.CreateLoan(owner);
                    break;

                case 3:


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
                    else if (Loan.IsLoanDeclinedMsg(owner) == true && !Admin.Loantickets.ContainsKey(owner))//Problem med inboxens villkor
                    {
                        Console.Clear();
                        Console.WriteLine("Du har ett nytt meddelande angående din låneansökan:\n");
                        owner.ShowInbox();
                        Utilities.startColoring(ConsoleColor.Red);
                        Console.WriteLine("\nMeddelande raderas när du återgår till menyn\n\n");
                        Utilities.stopColoring();
                        owner._inbox.Clear();
                        Utilities.NoContentMsg();

                    }
                    else
                    {
                        UnderMenuHeader("--Lånemeny--");
                        Console.WriteLine("Dina ärenden:\n");
                        owner.ShowInbox();
                        Console.WriteLine("\n Vad vill du göra?\n");
                        Console.WriteLine("1.Hantera ärende");
                        Console.WriteLine("2.Gå tillbaka");

                        int userchoice = Utilities.GetUserNumberMinMax(1, 2);
                        if (userchoice == 1)
                        {
                            bool accept = owner.HandleLoanSuggestion(1, owner);//Satte siffran 1 då användaren inte kan ha fler än 1 låneförfrågan åt gången just nu. [Daniel-01/12}
                            DatabaseAccounts.AddAllAccounts(Bank.GetUsers());
                            DatabaseAccounts.LoadAllAccounts(Bank.GetUsers());
                            break;
                        }
                        else
                            break;
                    }
                    break;

                case 4:
                    UnderMenuHeader("--Avbetalningar--");
                    Console.WriteLine("Här kan du göra amorteringar på dina lån.");
                    if (owner._loans.Count() == 0)
                    {

                        Console.WriteLine("Du har inga lån");
                        Utilities.NoContentMsg();
                        break;
                    }
                    else if (owner.TotalFunds() < 100)
                    {
                        Utilities.startColoring(ConsoleColor.DarkRed);
                        Console.WriteLine("Du har för lite tillgångar för att göra en amortering!");
                        Utilities.stopColoring();
                        Utilities.NoContentMsg();
                        break;
                    }
                    else
                    {
                        int counter = 1;
                        foreach (Loan loan in owner._loans)
                        {
                            Utilities.DashDivide();
                            Console.WriteLine(counter + ".");
                            Console.WriteLine($"Lån: {loan.Loanamount}Kr\nRäntesats: {loan.ShowLoanInterestrate()}%\nÅrlig räntekostnad: {(loan.ShowLoanInterestrate() / 100) * loan.Loanamount}Kr ");
                            Utilities.DashDivide();
                            counter++;
                        }
                        Console.WriteLine("\nVälj ett lån i listan att amortera eller ange siffra \"0\" för att avbryta för att återgå till huvudmenyn.\n");
                        Console.Write("Val:");
                        int loanselect = Utilities.GetUserNumberMinMax(0, owner._loans.Count());
                        if (loanselect == 0)
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            loanselect = loanselect - 1;
                            Console.Clear();
                            UnderMenuHeader("--Avbetalningar--");
                            Utilities.DashDivide();
                            Console.WriteLine($"Lån: {owner._loans[loanselect].Loanamount}Kr\nRäntesats: {owner._loans[loanselect].ShowLoanInterestrate()}%\nÅrlig räntekostnad: {(owner._loans[loanselect].ShowLoanInterestrate() / 100) * owner._loans[loanselect].Loanamount}Kr ");
                            Utilities.DashDivide();
                            Console.Write("\nVill du betala från ett sparkonto(1) eller ett vanligt konto?(2):");
                            int accounttypechoice = Utilities.GetUserNumberMinMax(1, 2);

                            if (accounttypechoice == 1)
                            {
                                Console.WriteLine();
                                Customer.ShowCustomerSavingAccounts(owner);
                                Console.WriteLine();
                                Console.Write($"Välj vilket konto du vill betala ifrån (1-{owner.GetSavingAccountList().Count()}):");
                                int accountchoice = Utilities.GetUserNumberMinMax(1, owner.GetSavingAccountList().Count());
                                var acctofiddlewith = owner.GetSavingAccountList();
                                decimal convertedcash = Currency.ConvertCurrency(acctofiddlewith[accountchoice - 1].GetCurrency(), "SEK", owner.ShowSavingAccountsFunds(accountchoice));

                                Console.WriteLine($"\nTänk på att du inte kan ammortera mer än 20% av ditt kontoinnehav åt gången.\nDitt maxbelopp för valt konto är just nu {convertedcash * 0.2m}SEK");
                                Console.Write("\nAnge amorteringsumma(SEK):");
                                decimal payment = Utilities.GetUserDecimalMinMax(1, (convertedcash * 0.2m));
                                decimal maxPayment = owner._loans[loanselect].Loanamount;
                                payment = Math.Min(payment, maxPayment);
                                owner._loans[loanselect].Loanamount = owner._loans[loanselect].Loanamount - payment;
                                if (owner._loans[loanselect].Loanamount <= 0)
                                {
                                    owner._loans.RemoveAt(loanselect);
                                    Console.WriteLine($"Du har gjort en avbetalning på {payment}SEK.\nLånet är nu avbetalt och kommer försvinna ur din översikt.");
                                    acctofiddlewith[accountchoice - 1].RemoveMoney(payment);
                                    Utilities.NoContentMsg();
                                }
                                else
                                {


                                    Console.WriteLine($"Du har gjort en avbetalning på {payment}SEK.\nKvarvarande summa på lån: {owner._loans[loanselect].Loanamount}SEK");
                                    payment = Currency.ConvertCurrency("SEK", acctofiddlewith[accountchoice - 1].GetCurrency(), payment);
                                    acctofiddlewith[accountchoice - 1].RemoveMoney(payment);
                                    Utilities.NoContentMsg();
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Customer.ShowCustomerAccounts(owner);
                                Console.WriteLine();
                                Console.Write($"Välj vilket konto du vill betala ifrån (1-{owner.GetAccountList().Count()}):");
                                int accountchoice = Utilities.GetUserNumberMinMax(1, owner.GetAccountList().Count());
                                var acctofiddlewith = owner.GetAccountList();
                                decimal convertedcash = Currency.ConvertCurrency(acctofiddlewith[accountchoice - 1].GetCurrency(), "SEK", owner.ShowAccountsFunds(accountchoice));

                                Console.WriteLine($"\nTänk på att du inte kan ammortera mer än 20% av ditt kontoinnehav åt gången.\nDitt maxbelopp för valt konto är just nu {convertedcash * 0.2m}SEK");
                                Console.Write("\nAnge amorteringsumma(SEK):");
                                decimal payment = Utilities.GetUserDecimalMinMax(1, (convertedcash * 0.2m));
                                decimal maxPayment = owner._loans[loanselect].Loanamount;
                                payment = Math.Min(payment, maxPayment);
                                owner._loans[loanselect].Loanamount = owner._loans[loanselect].Loanamount - payment;
                                if (owner._loans[loanselect].Loanamount <= 0)
                                {
                                    owner._loans.RemoveAt(loanselect);
                                    Console.WriteLine($"Du har gjort en avbetalning på {payment}SEK.\nLånet är nu avbetalt och kommer försvinna ur din översikt.");
                                    acctofiddlewith[accountchoice - 1].RemoveMoney(payment);
                                    Utilities.NoContentMsg();
                                }
                                else
                                {


                                    Console.WriteLine($"Du har gjort en avbetalning på {payment}SEK.\nKvarvarande summa på lån: {owner._loans[loanselect].Loanamount}SEK");
                                    payment = Currency.ConvertCurrency("SEK", acctofiddlewith[accountchoice - 1].GetCurrency(), payment);
                                    acctofiddlewith[accountchoice - 1].RemoveMoney(payment);
                                    Utilities.NoContentMsg();
                                }
                            }
                        }
                        break;
                    }

                default:
                    Console.Clear();
                    break;
            }

        }

        public static void DepositMenu(Customer user)
        {
            int count = 0;

            UnderMenuHeader("--Insättning--");

            if (user.GetAccountList().Count == 0)
            {
                Console.WriteLine("Du har inga aktiva konton");
            }
            else
            {
                Console.WriteLine($"{"Nr",-5} {"Konto",-25} {"Kontonummer",-20} {"Saldo",-10} {"Valuta",-10}");

                foreach (var account in user.GetAccountList())
                {
                    count++;
                    Console.WriteLine($"{count,-5} {account.Name,-25} {account.GetAccountNumber(),-20} {account.GetBalance(),-10} {account.GetCurrency(),-10}");
                }
                Console.WriteLine("\nVad vill du göra?");
                Console.WriteLine("1.Gör en insättning");
                Console.WriteLine("2.Gå tillbaka");
                int choice = Utilities.GetUserNumberMinMax(1, 2);

                if (choice == 1)
                {
                    Console.Write("\nVälj konto för insättning:");
                    int input = Utilities.GetUserNumberMinMax(1, count);
                    Account userChoice = user.GetAccountList()[input - 1];
                    Console.Write($"Ange insättningsbelopp({userChoice.GetCurrency()}): ");
                    decimal deposit = Utilities.GetUserDecimal();
                    userChoice.Deposit(deposit);
                    Console.WriteLine($"Lyckad insättning: {deposit} {userChoice.GetCurrency()} till konto {userChoice.Name}");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                }
            }
        }

        public static void UnderMenuHeader(string header)
        {
            Console.Clear();
            Utilities.startColoring(ConsoleColor.Yellow);
            Console.WriteLine(header);
            Utilities.stopColoring();
            Utilities.DashDivide();
            Console.WriteLine();
        }



    }
}
