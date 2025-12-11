using System.Security.Principal;

namespace The_Singletons_Bank
{
    internal class DatabaseAccounts
    {
        static string path = "C:\\Users\\liamb\\Desktop\\Shared Code\\Accounts.txt";

        public static void AddAllAccounts(List<User> users)
        {
            List<string> accounts = new List<string>();



            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    accounts.Add(reader.ReadLine());
                }
            }

            foreach (string account in accounts)
            {
                if (account == )
            }

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                foreach (User user in users)
                {
                    if (user is Customer customer)
                    {
                        foreach (Account account in customer.GetAccountList())
                        {
                            writer.WriteLine($"ACCOUNT;{customer.GetUsername()};{account.Name};{account.GetAccountNumber()};{account.GetBalance()};{account.GetCurrency()}");
                        }

                        foreach (SavingAccount savings in customer.GetSavingAccountList())
                        {
                            writer.WriteLine($"SAVING;{customer.GetUsername()};{savings.Name};{savings.GetAccountNumber()};{savings.GetBalance()};{savings.GetCurrency()};{savings.GetInterest()}");
                        }

                        foreach (Loan loan in customer.GetLoansList())
                        {
                            writer.WriteLine($"LOAN;{customer.GetUsername()};{loan.Loanamount};{loan.ShowLoanInterestrate()}");
                        }

                        foreach (String inbox in customer.GetInboxList())
                        {
                            writer.WriteLine($"INBOX;{customer.GetUsername()};{inbox}");
                        }
                    }
                }
            }
        }
        public static void LoadAllAccounts(List<User> users)
        {
            string line = "";

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();

                    string[] parts = line.Split(";");

                    string type = parts[0];
                    string username = parts[1];

                    var user = users.FirstOrDefault(u => u.GetUsername() == username);
                    if (user is not Customer customer)
                        continue;

                    switch (type)
                    {
                        case "ACCOUNT":
                            string name = parts[2];
                            decimal balance = decimal.Parse(parts[4]);
                            string currency = parts[5];

                            var acc = new Account(name, balance, currency);
                            customer.AddToAccountList(acc);
                            break;

                        case "SAVING":
                            string nameS = parts[2];
                            decimal BalanceS = decimal.Parse(parts[4]);
                            decimal rate = decimal.Parse(parts[6]);

                            var sav = new SavingAccount(nameS, BalanceS, rate);
                            customer.AddToSavingAccountList(sav);
                            break;

                        case "LOAN":
                            decimal amount = decimal.Parse(parts[2]);
                            decimal rateL = decimal.Parse(parts[3]);

                            var loan = new Loan(customer, rateL, amount);
                            customer.AddToLoanList(loan);
                            break;

                        case "INBOX":
                            string message = parts[2];
                            customer.AddToInboxList(message);
                            break;
                    }
                }
            }
        }
        public static string GetAccountString(User user)
        {
            string line = "";
            return "";
            if (user is Customer customer)
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();

                        string[] parts = line.Split(';');

                        string type = parts[0];

                        foreach (Account account in customer.GetAccountList())
                        {
                            return $"ACCOUNT;{customer.GetUsername()};{account.Name};{account.GetAccountNumber()};{account.GetBalance()};{account.GetCurrency()}";
                        }
                    }
                }
            }
        }        
    }
}
