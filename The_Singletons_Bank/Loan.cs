using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Loan
    {
        public static Dictionary<Customer, Loan> PendingLoans = new Dictionary<Customer, Loan>();
        private decimal Interestrate { get; set; }

        //private int LoanNumber { get; set; }
        private decimal Loanamount { get; set; }

        public Loan(Customer owner, decimal interestrate, decimal loanamount)
        {
            Interestrate = interestrate;
            Loanamount = loanamount;
            //LoanNumber = loannumber;//Later we should add function to number/name loans
        }


        public string ShowLoandetails()
        {
            string loandetails = ($"Ränta: {Interestrate}\nLånebelopp: {Loanamount}\nKostnad för lån: {Loanamount}*{Interestrate / 100}");
            return loandetails;

        }


        public static void ShowLoanMenu(Customer owner)
        {
            Console.Clear();
            Console.WriteLine("1.Visa mina lån");
            Console.WriteLine("2.Visa pågående ärenden");
            Console.WriteLine("3.Ta nytt lån");
            Console.WriteLine("4.Gå tillbaka");
            int choice = Utilities.GetUserNumberMinMax(1, 3);

            switch (choice)
            {
                case 1:
                    if (owner._loans.Count == 0)
                    {
                        Utilities.DashDivide();
                        Console.WriteLine($"Lån: {loan.Loanamount}Kr\nRäntesats: {loan.ShowLoanInterestrate()}%\nLånekostnad: {(loan.ShowLoanInterestrate() / 100) * loan.Loanamount}Kr ");
                        Utilities.DashDivide();
                    }
                    else
                    {
                        Console.WriteLine("Mina lån\n");
                        foreach (Loan loan in owner._loans)
                        {
                            Utilities.DashDivide();
                            Console.WriteLine($"Lån: {loan.Loanamount}Kr\nRäntesats: {loan.ShowLoanInterestrate()}%\nLånekostnad: {(Interestrate / 100) * loan.Loanamount}Kr ");
                            Utilities.DashDivide();
                        }
                    }                        
                    break;

                case 2:
                    owner.ShowInbox();
                    Console.WriteLine("\n Vad vill du göra?\n");
                    Console.WriteLine("1.Se status för lånförfrågan");
                    Console.WriteLine("2.Gå tillbaka");

                    int userchoice = Utilities.GetUserNumberMinMax(1, 2);
                    if (userchoice == 1)
                    {
                        Console.Write("Välj lån i listan:");
                        int loanchoice = Utilities.GetUserNumberMinMax(1, owner._inbox.Count());
                        bool accept = owner.HandleLoanSuggestion(loanchoice.ToString(), owner);
                        break;
                    }
                    else
                        break;

                case 3:
                    Console.WriteLine("Ange önskat lånebelopp:");
                    CreateTestLoan(owner);
                    break;

                default:
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    break;
            }



        }


        public decimal ShowLoanInterestrate()
        {
            return Interestrate;
        }

        //public static Loan CreateLoan(Customer owner)
        //{
        //    decimal loanamount = Utilities.GetUserNumber();

        //    bool ok = Loangranted(loanamount, owner.ShowBalance());

        //    if (ok)
        //    {
        //        Console.WriteLine($"Ditt lån kan bli beviljat till en ränta på {Interestrate}%.\n Total kostnad för lån: {(Interestrate / 100) * loanamount}Kr\n" +
        //            $"Godkänner du detta vilkor?");

        //        string choice = Utilities.GetUserChoiceYN();
        //        if (choice == "y")
        //        {
        //            Loan loan = new Loan(owner, Interestrate, loanamount);
        //            Console.WriteLine($"Du har lånat {loanamount}SEK till en ränta av {Interestrate}%");
        //            //Console.WriteLine("Välj konto för insättning:");
        //            //transaction.Send(loan1.Loanamount); Send money to correct account with transactionclass - Daniel [21/11-25]
        //            owner._loans.Add(loan);
        //            return loan;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Låneförfrågan avbruten.\nTryck på valfri tangent för att gå tillbaka...");
        //            Console.ReadLine();
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Din låneförfrågan överskrider din maxgräns på {owner.ShowBalance() * 5}Kr.\nSänk ditt belopp för att göra en ny förfrågan.\n" +
        //            $"Tryck på valfri tangent för att gå tillbaka...");
        //        Console.ReadLine();
        //        return null;
        //    }
        //}
        //public static bool Loangranted(decimal loanRequest, decimal balance)
        //{

        //    if (loanRequest > (balance * 5))
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public static void CreateTestLoan(Customer owner)
        {
            decimal loanamount = Utilities.GetUserNumber();
            bool limitOk = Loangrantedtest(loanamount, owner.ShowBalance());
            bool hasActiveTicket = HasActiveTicket(owner);

            if (limitOk && hasActiveTicket==false)
            {

                Console.WriteLine($"Din låneförfrågan på {loanamount}SEK har skickats till banken. Ditt ärende hanteras inom 1-3 bankdagar.");
                Admin.Loantickets.Add(owner, loanamount);//Om en låneförfrågan redan finns så kan man inte göra en ny PGA samma key redan finns.

            }
            else if (limitOk == false && hasActiveTicket==false)
            {
                Console.WriteLine($"Din låneförfrågan överskrider din maxgräns på {owner.ShowBalance() * 5}Kr.\nSänk ditt belopp för att göra en ny förfrågan.\n" +
                    $"Tryck på valfri tangent för att gå tillbaka...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Du har redan en aktiv låneförfrågan. Denna måste hanteras innan du kan ta ett nytt lån.");

            }
        }

        public static bool Loangrantedtest(decimal loanRequest, decimal balance)
        {
            if (loanRequest > (balance * 5))
            {
                return false;
            }
            return true;
        }

        public static bool HasActiveTicket(Customer owner)
        {
            if (Admin.Loantickets.ContainsKey(owner))
            {
                return false;
            }
            else
                return true;
        }

    }
}

