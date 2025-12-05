using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Loan
    {
        public static Dictionary<Customer, Loan> PendingLoans = new Dictionary<Customer, Loan>();
        private decimal Interestrate { get; set; }

        public decimal Loanamount { get; set; }

        public Loan(Customer owner, decimal interestrate, decimal loanamount)
        {
            Interestrate = interestrate;
            Loanamount = loanamount;
        }


        public string ShowLoandetails()
        {
            string loandetails = ($"Ränta: {Interestrate}%\nLånebelopp: {Loanamount}SEK\nKostnad för lån: {Loanamount*(Interestrate / 100)}SEK");
            return loandetails;

        }

        public decimal ShowLoanInterestrate()
        {
            return Interestrate;
        }

        public static void CreateLoan(Customer owner)
        {
            decimal loanamount = Utilities.GetUserDecimal();
            bool limitOk = Loangrantedtest(loanamount, owner.TotalFunds());
            bool hasActiveTicket = HasActiveTicket(owner);

            if (limitOk && hasActiveTicket == false)
            {

                Console.WriteLine($"\nDin låneförfrågan på {loanamount} SEK har skickats till banken. Ditt ärende hanteras inom 1-3 bankdagar.");
                Admin.Loantickets.Add(owner, loanamount);//Om en låneförfrågan redan finns så kan man inte göra en ny PGA samma key redan finns.
                Utilities.NoContentMsg();

            }
            else if (limitOk == false && hasActiveTicket == false)
            {
                Console.WriteLine($"Din låneförfrågan överskrider din maxgräns på {owner.TotalFunds() * 5}Kr.\nSänk ditt belopp för att göra en ny förfrågan.");
                Utilities.NoContentMsg();
            }
            else
            {
                Console.WriteLine("Du har redan en aktiv låneförfrågan. Denna måste hanteras innan du kan ta ett nytt lån.");
                Utilities.NoContentMsg();

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
            if (Admin.Loantickets.ContainsKey(owner)||owner._inbox.Count()!=0)//Detta villkor måste ändras om vi vill använda inbox till nått annat än lån [Daniel 28/11]
            {
                return true;
            }
            else
                return false;
        }

      public static bool IsLoanDeclinedMsg(Customer owner)
        {
            foreach (var msg in owner._inbox)
            {
                if (msg.Contains("Ränta:"))
                {
                    return false;

                }
                else
                {
                    return true;
                }
            }
            return false;
        }


    }
}

