using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Customer : User
    {
        private List<Account> _accounts;
        private List<SavingAccount> _savingAccounts;
        public List<Loan> _loans;
        public List<String> _inbox;

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
            foreach (string letter in _inbox)
            {
                Utilities.DashDivide();
                Console.WriteLine(counter + ".");
                Console.WriteLine(letter);
                Utilities.DashDivide();
                counter++;
            }
        }

        public bool HandleLoanSuggestion(string letter, Customer customer)
        {
            Console.WriteLine("Accepterar du lånevillkoren för valt lån?");
            string userchoice = Utilities.GetUserChoiceYN();
            if (userchoice == "y")
            {
                if (Loan.PendingLoans.ContainsKey(customer))
                {
                    Console.WriteLine("Du accepterade lånevillkoren. Du kan se ditt lån under \"mina lån\"");
                    Loan loan = Loan.PendingLoans[customer]
                    ;
                    _loans.Add(loan);
                    _inbox.Remove(letter);
                    Loan.PendingLoans.Remove(customer);
                }
                return true;
            }
            else
            {
                Console.WriteLine("Du nekade låneförslaget");
                _inbox.Remove(letter);
                Loan.PendingLoans.Remove(customer);
                return false;
            }
        }

        public List<Account> GetAccountList()
        {
            return _accounts;
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

        



    }

}
