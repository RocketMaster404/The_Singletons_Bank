using System.Security.Principal;

namespace The_Singletons_Bank
{
    internal class DatabaseAccounts
    {
        static string path = "C:\\Users\\liamb\\Desktop\\Shared Code\\Accounts.txt";

        public static void AddAllAccounts(List<User> users)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (User user in users)
                {
                    if (user is Customer customer)
                    {
                        foreach (Account account in customer.GetAccountList())
                        {
                            writer.WriteLine($"ACCOUNT;{customer.GetUsername()};{account.Name};{account.GetAccountNumber};{account.GetBalance()};{account.GetCurrency()}");
                        }
                        foreach (SavingAccount savings in customer.GetSavingAccountList())
                        {
                            writer.WriteLine($"SAVING;{customer.GetUsername()};{savings.Name};{savings.GetAccountNumber};{savings.GetBalance()};{savings.GetCurrency()};{savings.GetInterest()}");
                        }
                        foreach (Loan loan in customer.GetLoansList())
                        {
                            writer.WriteLine($"LOAN;{customer.GetUsername()};{loan.Loanamount};{loan.ShowLoanInterestrate()}");
                        }
                    }
                }
            }
        }                
    }
}
