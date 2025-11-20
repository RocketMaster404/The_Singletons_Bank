using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Loans
    {
       internal static bool LoanLimit;

        
        public static void TakeLoan(decimal loan)
        {
            LoanLimit=Loanrequest(loan);

            if (LoanLimit = true)
            {
                Console.WriteLine($"Ditt lån har blivit beviljat.\nDu har lånat -{loan}Kr");
                Account.Balance = Account.Balance + loan;

            }

            else
            {
                Console.WriteLine($"Du kan inte låna mer än" + { Account.balance}*5+"\nDitt lån har blivit nekat");
            }
        }

        public static bool Loanrequest(decimal loanRequest)
        {
            int balance = Account.balance;
            if (loanRequest >= (balance * 5))
            {
                return false;
            }
            return true;
        }

    }
}
