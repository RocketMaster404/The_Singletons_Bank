using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
   internal class Account
   {
      private static List<int> usedAccountNumbers = new();
      public string Name { get; set; } = string.Empty;
      private int _accountNumber { get; set; }
      private decimal _balance { get; set; }
      private string _currency { get; set; } = "SEK";

      public Account(string name,decimal balance, string currency)
      {
         Name = name;
         _accountNumber = GenerateAccountNumber();
         _balance = balance;
         _currency = currency;
          
      }

      public int GetAccountNumber()
      {
         return _accountNumber;
      }

      public decimal GetBalance()
      {
         return _balance;
      }

      public void AddMoney(decimal ammount)
      {
          _balance += ammount;
      }

      public void RemoveMoney(decimal ammount)
      {
            _balance -= ammount;
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

      public static void ShowAccount (Account account) 
      {
         Console.WriteLine($"Konto: {account.Name}");
         Console.WriteLine($"Kontonummer: {account._accountNumber}");
         Console.WriteLine($"Saldo: {account._balance} {account._currency}");
         Console.WriteLine("**************************");
      }

      

      public  static void CreateAccount(Customer user) 
      {
         Console.WriteLine("Skapa konto");
         Console.WriteLine("1. Konto i SEK");
         Console.WriteLine("2. Konto i USD");
         Console.WriteLine("3. Konto i EUR");
         Console.Write("Ange valuta: ");
         int input = Utilities.GetUserNumberMinMax(1, 3);
         string name;

         switch (input)
         {
            case 1:
               Console.WriteLine("Ange namnet på ditt konto");
               name = Console.ReadLine();
               var accountSEK = new Account(name,1000, "SEK");
               Console.WriteLine("Konto skapat\n");
               ShowAccount(accountSEK);
               user.AddToAccountList(accountSEK);
               break;
               
            case 2:
               Console.WriteLine("Ange namnet på ditt konto");
               name = Console.ReadLine();
               var accountUSD = new Account(name,10, "USD");
               Console.WriteLine("Konto skapat\n");
               ShowAccount(accountUSD);
               user.AddToAccountList(accountUSD);
               break;

            case 3:
               Console.WriteLine("Ange namnet på ditt konto");
               name = Console.ReadLine();
               var accountEUR = new Account(name,10, "EUR");
               Console.WriteLine("Konto skapat\n");
               ShowAccount(accountEUR);
               user.AddToAccountList(accountEUR);
               break;

         }

         
      }


   }
}
