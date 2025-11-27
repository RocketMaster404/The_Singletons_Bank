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

        public string GetCurrency()
        {
            return _currency;
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

      public void changeBalance(decimal balance)
      {
         _balance = balance;
      }

      public static Account CreateAccount(string name,decimal balance, string currency,Customer user)
      {
         var account = new Account(name, balance, currency);
         user.AddToAccountList(account);
         return account;   
      }

      

     


   }
}
