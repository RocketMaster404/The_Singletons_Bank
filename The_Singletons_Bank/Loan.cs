using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Loan
    {

        public static decimal Interestrate { get; private set; } = 2.4m;//Interest is now static.... should it? 

        private decimal Loanamount { get; set; }



        public Loan(Customer owner, decimal interestrate, decimal loanamount)
        {
            Interestrate = interestrate;
            Loanamount = loanamount;

        }

        public static void ShowLoanMenu(Customer owner)
        {
            Console.WriteLine("1.Visa lån");
            Console.WriteLine("2.Ta nytt lån");
            Console.WriteLine("3.Gå tillbaka");
            int choice = Utilities.GetUserNumberMinMax(1, 3);

            switch (choice)
            {
                case 1:
                    Console.WriteLine("lånelista");
                    break;

                case 2:
                    Console.WriteLine("Ange önskat lånebelopp:");
                    CreateLoan(owner);
                    break;

                default:
                    break;
            }



        }

        public static Loan CreateLoan(Customer owner)
        {
            decimal loanamount = Utilities.GetUserNumber();

            bool ok = Loangranted(loanamount, owner.ShowBalance());

            if (ok)
            {
                Console.WriteLine($"Ditt lån kan bli beviljat till en ränta på {Interestrate}%.\n Total kostnad för lån: {(Interestrate / 100) * loanamount}Kr\n" +
                    $"Godkänner du detta vilkor? (Y/N)");

                string choice = Utilities.GetUserChoiceYN();
                if (choice == "y")
                {
                    Loan loan = new Loan(owner, Interestrate, loanamount);
                    Console.WriteLine($"Du har lånat {loanamount} till en ränta av {Interestrate}%");
                    //Console.WriteLine("Välj konto för insättning:");
                    //transaction.Send(loan1.Loanamount); Send money to correct account with transactionclass - Daniel [21/11-25]
                    return loan;
                }
                else
                {
                    Console.WriteLine("Låneförfrågan avbruten.\nTryck på valfri tangent för att gå tillbaka...");
                    Console.ReadLine();
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Din låneförfrågan överskrider din maxgräns på {owner.ShowBalance() * 5}Kr.\nSänk ditt belopp för att göra en ny förfrågan.\n" +
                    $"Tryck på valfri tangent för att gå tillbaka...");
                Console.ReadLine();
                return null;
            }
        }
        public static bool Loangranted(decimal loanRequest, decimal balance)
        {

            if (loanRequest > (balance * 5))
            {
                return false;
            }
            return true;
        }



    }
}
