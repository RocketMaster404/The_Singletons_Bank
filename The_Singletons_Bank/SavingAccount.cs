using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
   internal class SavingAccount : Account
   {
      private static decimal _interestRate { get; set; } = 1.4m;

      public SavingAccount(string name,decimal balance):base(name,balance,"SEK")
      {
         
      }

      public decimal GetInterest()
      {
         return _interestRate;
      }

      public static void CreateSavingAccount(Customer user) // Måste adderas till users listan
      {
         
         Console.WriteLine($"Skapa sparkonto med ränta {_interestRate}");
         Console.WriteLine("Vill du skapa ett konto? y/n");
         string input = Console.ReadLine().ToLower();

         if(input == "y")
         {
            Console.WriteLine("Ange namnet på ditt konto: ");
            string name = Console.ReadLine();
            var savingAccount = new SavingAccount(name,0);
            Console.WriteLine($"Du har skapat ett sparkonto");
            ShowAccount(savingAccount);
            user.AddToSavingAccountList(savingAccount);
           
         }

         

      }

      public void IncreaseBalance()
      {
         decimal balance = GetBalance();
         decimal newBalance = balance * (1 + _interestRate / 100);
         changeBalance(newBalance);
         
      }

      public static void ShowSavingAccountInfo(Account account)
      {
         Console.WriteLine($"Sparkonto: {account.Name}");
         Console.WriteLine($"Kontonummer: {account.GetAccountNumber()}");
         Console.WriteLine($"Saldo: {account.GetBalance()} Kr");
         Console.WriteLine("**************************");
      }

   }
}
