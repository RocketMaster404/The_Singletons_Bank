using Microsoft.VisualBasic;
using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace The_Singletons_Bank
{
    internal class DatabaseAccounts
    {
        static string pathAccounts = "Accounts.txt";        

        public static void AddAllAccounts(List<User> users)
        {
            List<string> existingAccounts = new List<string>();
            List<string> compareAccounts = new List<string>();
            List<string> addAccounts = new List<string>();            

            using (StreamReader reader = new StreamReader(pathAccounts))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(';');
                    string type = parts[0];

                    string completeString = "";

                    switch (type)
                    {
                        case "ACCOUNT":
                            completeString = $"{parts[0]};{parts[1]};{parts[2]};{parts[4]};{parts[5]}";
                            break;

                        case "SAVING":
                            completeString = $"{parts[0]};{parts[1]};{parts[2]};{parts[4]};{parts[5]};{parts[6]}";
                            break;

                        case "LOAN":
                            completeString = $"{parts[0]};{parts[1]};{parts[2]};{parts[3]}";
                            break;

                        case "INBOX":
                            completeString = $"{parts[0]};{parts[1]};{parts[2]}";
                            break;
                    }

                    existingAccounts.Add(completeString);
                }
            }

            foreach (User user in users)
            {
                if (user is Customer customer)
                {
                    foreach (Account rawAccount in customer.GetAccountList())
                    {
                        string line = GetAccountString(rawAccount, customer);
                        string[] parts = line.Split(';');

                        string add = $"{parts[0]};{parts[1]};{parts[2]};{parts[4]};{parts[5]}";
                        string addFile = $"{parts[0]};{parts[1]};{parts[2]};{parts[3]};{parts[4]};{parts[5]}";
                        compareAccounts.Add(add);
                        addAccounts.Add(addFile);
                    }

                    foreach (SavingAccount rawSaveAccount in customer.GetSavingAccountList())
                    {
                        string line = GetSavingString(rawSaveAccount, customer);
                        string[] parts = line.Split(';');

                        string add = $"{parts[0]};{parts[1]};{parts[2]};{parts[4]};{parts[5]};{parts[6]}";
                        string addFile = $"{parts[0]};{parts[1]};{parts[2]};{parts[3]};{parts[4]};{parts[5]};{parts[6]}";
                        compareAccounts.Add(add);
                        addAccounts.Add(addFile);
                    }

                    foreach (Loan rawLoan in customer.GetLoansList())
                    {
                        string line = GetLoanString(rawLoan, customer);
                        string[] parts = line.Split(';');

                        string add = $"{parts[0]};{parts[1]};{parts[2]};{parts[3]}";
                        string addFile = $"{parts[0]};{parts[1]};{parts[2]};{parts[3]}";
                        compareAccounts.Add(add);
                        addAccounts.Add(addFile);
                    }

                    foreach (string msg in customer.GetInboxList())
                    {
                        string line = GetInboxString(msg, customer);
                        string[] parts = line.Split(';');

                        string add = $"{parts[0]};{parts[1]};{parts[2]}";
                        string addFile = $"{parts[0]};{parts[1]};{parts[2]}";
                        compareAccounts.Add(add);
                        addAccounts.Add(addFile);
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter(pathAccounts, true))
            {
                for (int i = 0; i < compareAccounts.Count; i++)
                {
                    string compare = compareAccounts[i];

                    if (!existingAccounts.Contains(compare))
                    {
                        string fullLine = addAccounts[i];
                        writer.WriteLine(fullLine);
                    }
                }
            }
        }
        public static void LoadAllAccounts(List<User> users)
        {
            string line = "";

            using (StreamReader reader = new StreamReader(pathAccounts))
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
        public static string GetAccountString(Account account, Customer customer)
        {
            return $"ACCOUNT;{customer.GetUsername()};{account.Name};{account.GetAccountNumber()};{account.GetBalance()};{account.GetCurrency()}";
        }
        public static string GetSavingString(SavingAccount savings, Customer customer)
        {
            return $"SAVING;{customer.GetUsername()};{savings.Name};{savings.GetAccountNumber()};{savings.GetBalance()};{savings.GetCurrency()};{savings.GetInterest()}";
        }
        public static string GetLoanString(Loan loan, Customer customer)
        {
            return $"LOAN;{customer.GetUsername()};{loan.Loanamount};{loan.ShowLoanInterestrate()}";
        }
        public static string GetInboxString(string inbox, Customer customer)
        {
            return $"INBOX;{customer.GetUsername()};{inbox}";
        }
        public static void CreateFileAccount()
        {
            StreamWriter writer = new StreamWriter(pathAccounts, true);
            writer.Close();
        }
    }
}
