using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Loan
    {

        public decimal Interestrate { get; private set; }=2.4m;

        private decimal Loanamount { get; set; }



        public Loan(Customer owner, decimal interestrate, decimal loanamount)
        {
            Interestrate = interestrate;
            Loanamount = loanamount;

        }

        //public decimal Showinterestrate()
        //{
        //    return Interestrate;
        //}

        public static void ShowLoanMenu()
        {
            Console.WriteLine("1.Visa lån");
            Console.WriteLine("2.Ta nytt lån");
            int choice = Utilities.GetUserNumberMinMax(1, 2);

            if (choice == 1)
            {
                //CreateLoan();
            }

        }

        public Loan CreateLoan(Customer owner)
        {
            Console.WriteLine("Hur mycket vill du låna?");

            decimal loan;
            while (!decimal.TryParse(Console.ReadLine(), out loan))
            {
                Console.WriteLine("Ange heltal i tKr");
            }

            bool ok = Loangranted(loan, owner.ShowBalance());

            if (ok == true)
            {
                Console.WriteLine($"Ditt lån kan bli beviljat till en ränta på {Interestrate}%.\n Total kostnad för lån: {(Interestrate/100) * loan}Kr\n" +
                    $"Godkänner du detta vilkor? (Y/N)");

                string choice=Utilities.GetUserChoiceYN();
                if (choice == "y")
                {
                    Loan loan1 = new Loan(owner, Interestrate, loan);
                    Console.WriteLine($"Du har lånat {loan} till en ränta av {Interestrate}%");

                    //Console.WriteLine("Välj konto för insättning:");
                    //transaction.Send(loan1.Loanamount); Send money to correct account with transactionclass - Daniel [21/11-25]
                    return loan1;

                }
                else 
                {
                    Console.WriteLine("Låneförfrågan avbruten.");
                    return null;
                }

            }
            else
            {
                Console.WriteLine($"Din låneförfrågan överskrider din maxgräns på {owner.ShowBalance() * 5}Kr.\nSänk ditt belopp för att göra en ny förfrågan.");
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


        //internal double Interestrate;


        //public static void TakeLoan(Account user)
        //{
        //    decimal loanlimit=user.ShowAccBalance();
        //    LoanLimit = Loanrequest(loanlimit);

        //    if (LoanLimit = true)
        //    {
        //        Console.WriteLine($"Ditt lån har blivit beviljat.\nDu har lånat -{loan}Kr");
        //         =  + loan;

        //    }

        //    else
        //    {
        //        Console.WriteLine($"Du kan inte låna mer än" + { account.balance}
        //        *5 + "\nDitt lån har blivit nekat");
        //    }
        //}

        //public static bool Loanrequest(decimal loanRequest)
        //{
        //    decimal balance =;
        //    if (loanRequest >= (balance * 5))
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
