using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static The_Singletons_Bank.TransactionHistory;

namespace The_Singletons_Bank
{
    internal class Customer : User
    {
        private List<Account> _accounts;
        private List<SavingAccount> _savingAccounts;
        public List<Loan> _loans;
        public List<String> _inbox;
        public int CreditCred { get; private set; } = 100;

        public Customer(string username, string password) : base(username, password)//Ska listorna va med i konstruktorn?
        {
            _accounts = new List<Account>();
            _savingAccounts = new List<SavingAccount>();
            _loans = new List<Loan>();
            _inbox = new List<string>();

        }

        public void ShowInbox()
        {
            int counter = 1;
            if (_inbox.Count() != 0)
            {
                foreach (string letter in _inbox)
                {
                    Utilities.DashDivide();
                    Console.WriteLine(counter + ".");
                    Console.WriteLine(letter);
                    Utilities.DashDivide();
                    counter++;
                }
            }
        }

        public bool HandleLoanSuggestion(int letterchoice, Customer customer)
        {
            Console.WriteLine("Accepterar du lånevillkoren för valt lån?");
            string userchoice = Utilities.GetUserChoiceYN();
            if (userchoice == "y")
            {
                if (Loan.PendingLoans.ContainsKey(customer))
                {
                    Console.Clear();
                    Console.WriteLine("Du accepterade lånevillkoren. Du kan se ditt lån under \"mina lån\"");
                    Loan loan = Loan.PendingLoans[customer];
                    customer._loans.Add(loan);
                    customer._inbox.RemoveAt(letterchoice - 1);
                    Loan.PendingLoans.Remove(customer);
                    Utilities.NoContentMsg();
                }
                return true;
            }
            else
            {
                if (Loan.PendingLoans.ContainsKey(customer))
                {
                    Console.Clear();
                    Console.WriteLine("Du nekade låneförslaget");
                    customer._inbox.RemoveAt(letterchoice - 1);
                    Loan.PendingLoans.Remove(customer);
                    Utilities.NoContentMsg();
                    return false;
                }
                return false;
            }


        }

        public decimal TotalFunds()
        {
            decimal total = 0;
            foreach (Account funds in _accounts)
            {
                total = (total + funds.GetBalance());
            }
            foreach (Account funds in _savingAccounts)
            {
                total = (total + funds.GetBalance());
            }
            return total;
        }

        public List<Account> GetAccountList()
        {
            return _accounts;
        }

        public List<SavingAccount> GetSavingAccountList()
        {
            return _savingAccounts;
        }

        public void AddToAccountList(Account account)
        {
            _accounts.Add(account);
        }

        public void AddToSavingAccountList(SavingAccount account)
        {
            _savingAccounts.Add(account);
        }

        public static void ShowCustomerAccounts(Customer user)
        {
            if (user._accounts.Count == 0)
            {
                Console.WriteLine("Du har inga aktiva konton");
            }

            foreach (var account in user._accounts)
            {
                Account.ShowAccount(account);
            }
        }

        public static void ShowCustomerSavingAccounts(Customer user)
        {
            if (user._savingAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga aktiva sparkonton");
            }

            foreach (var account in user._savingAccounts)
            {
                SavingAccount.ShowSavingAccountInfo(account);
            }
        }

        public string CredibilityCalculator()

        {

            //Räknar först kredittrovärdighet baserat på lån
            foreach (Loan loan in _loans)
            {
                if (loan.Loanamount >= TotalFunds() * 5)
                {
                    CreditCred = Math.Max(CreditCred - 40, 0);//Tar det högsta värdet och returnerar så det aldrig kan gå under 0
                }
                else if (loan.Loanamount >= TotalFunds() * 4)
                {
                    CreditCred = Math.Max(CreditCred - 30, 0);
                }
                else if (loan.Loanamount >= TotalFunds() * 3)
                {
                    CreditCred = Math.Max(CreditCred - 20, 0);
                }
                else if (loan.Loanamount >= TotalFunds() * 2)
                {
                    CreditCred = Math.Max(CreditCred - 30, 0);
                }
                else
                {
                    CreditCred = Math.Max(CreditCred - 10, 0);
                }
            }
            //Räknar sen på sparkonton. Har man ett sparkonto går trovärdigheten upp
            if (_savingAccounts.Count > 0)
            {
                CreditCred = Math.Min(CreditCred + 20, 100);
            }
            //Räkna ut hur många uttag.
            foreach (TransactionHistory transaction in Transaction.GetQueue())
            {
                if (transaction.Type == TransferType.Deposit)
                {
                    CreditCred = Math.Max(CreditCred - 10, 0);
                }
            }
            //Räkna på antal insättingar:
            foreach (Account account in _accounts)
            {
                if (account.Depositcounter != 0)
                {
                    CreditCred = Math.Min((CreditCred + 10) * account.Depositcounter, 100);
                }
            }

            //Returnerar trovärdighet efter uträknad total:
            if (CreditCred >= 70)
            {
                Utilities.startColoring(ConsoleColor.DarkGreen);
                string high = "Hög";
                return high;
            }
            else if (CreditCred > 40 && CreditCred < 70)
            {
                Utilities.startColoring(ConsoleColor.DarkYellow);
                string mid = "Medel";
                return mid;
            }
            else
            {
                Utilities.startColoring(ConsoleColor.DarkRed);
                string low = "Låg";
                return low;
            }
        }





    }

}
