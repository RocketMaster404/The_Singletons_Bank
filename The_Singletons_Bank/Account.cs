using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    public class Account
    {
        private static List<int> usedAccountNumbers = new();
        private int _accountNumber { get; set; }
        private decimal _balance { get; set; }
        private string _currency { get; set; } = "SEK";

        public Account(decimal balance, string currency)
        {
            _accountNumber = GenerateAccountNumber();
            _balance = balance;
            _currency = currency;

        }

        public decimal ShowAccBalance()
        {
            return _balance;
        }
        public int GenerateAccountNumber()
        {
            Random random = new Random();
            bool uniqueNumber;
            int number;
            do
            {
                uniqueNumber = true;
                number = random.Next(10000000, 99999999);
                foreach (var existingAccountNumber in usedAccountNumbers)
                {
                    if (number == existingAccountNumber)
                    {
                        uniqueNumber = false;
                    }
                }

            } while (!uniqueNumber);

            usedAccountNumbers.Add(number);
            return number;
        }

        public void ShowAccount(Account account)
        {
            Console.WriteLine($"Kontoöversikt\n");
            Console.WriteLine($"Kontonummer: {account._accountNumber}");
            Console.WriteLine($"Saldo: {account._balance} {account._currency}");
        }

        public void CreateAccount(User user) // Behöver fixas så att man adderar till users lista
        {
            Console.WriteLine("Skapa konto");
            Console.WriteLine("1. Konto i SEK");
            Console.WriteLine("2. Konto i USD");
            Console.WriteLine("3. Konto i EUR");
            Console.Write("Ange valuta: ");
            int input = Utilities.GetUserNumberMinMax(1, 3);

            switch (input)
            {
                case 1:
                    var accountSEK = new Account(1000, "SEK");
                    Console.WriteLine("Konto skapat\n");
                    ShowAccount(accountSEK);
                    // Lägg till konto till users lista
                    break;

                case 2:
                    var accountUSD = new Account(10, "USD");
                    Console.WriteLine("Konto skapat\n");
                    ShowAccount(accountUSD);
                    // Lägg till konto till users lista
                    break;

                case 3:
                    var accountEUR = new Account(10, "EUR");
                    Console.WriteLine("Konto skapat\n");
                    ShowAccount(accountEUR);
                    // Lägg till konto till users lista
                    break;

            }


        }


    }
}
