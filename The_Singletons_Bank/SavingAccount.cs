using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
   internal class SavingAccount : Account
   {
      private  decimal _interestRate { get; set; }

      public SavingAccount(string name,decimal balance, decimal interest):base(name,balance,"SEK")
      {
         _interestRate = interest;
      }
      

     

      

      

      public decimal GetInterest()
      {
         return _interestRate;
      }

     

      public  static void CreateSavingAccount(Customer user) // Måste adderas till users listan
      {
         decimal interest1 = 1.5m;
         decimal interest2 = 1.7m;
         decimal interest3 = 2.0m;
         string name;

         Console.WriteLine("Vilken sparkonto typ önska du öpppna");
         Console.WriteLine($"1. Singelton standard: {interest1}");
         Console.WriteLine($"2. Singelton medel: {interest2}");
         Console.WriteLine($"3. Singelton långsiktigt: {interest3}");
         int input = Utilities.GetUserNumberMinMax(1, 3);

         switch (input)
         {
            case 1:
               Console.WriteLine("Ange namnet på ditt konto: ");
               name = Console.ReadLine();
               var savingAccount1 = new SavingAccount(name,0,interest1);
               Console.WriteLine($"Du har skapat ett sparkonto");
               ShowAccount(savingAccount1);
               user.AddToSavingAccountList(savingAccount1);
               break;
            case 2:
               Console.WriteLine("Ange namnet på ditt konto: ");
               name = Console.ReadLine();
               var savingAccount2 = new SavingAccount(name, 0, interest2);
               Console.WriteLine($"Du har skapat ett sparkonto");
               ShowAccount(savingAccount2);
               user.AddToSavingAccountList(savingAccount2);
               break;
            case 3:
               Console.WriteLine("Ange namnet på ditt konto: ");
               name = Console.ReadLine();
               var savingAccount3 = new SavingAccount(name, 0, interest3);
               Console.WriteLine($"Du har skapat ett sparkonto");
               ShowAccount(savingAccount3);
               user.AddToSavingAccountList(savingAccount3);
               break;
         }



         

         

      }

      public void IncreaseBalanceInterestRate()
      {
         decimal balance = GetBalance();
         decimal newBalance = balance * ( _interestRate / 100);
         changeBalance(newBalance);
         
      }

   

     

      public static void ShowSavingAccountInfo(SavingAccount account)
      {
        
         Console.WriteLine($"Sparkonto: {account.Name}");
         Console.WriteLine($"Kontonummer: {account.GetAccountNumber()}");
         Console.WriteLine($"Saldo: {account.GetBalance()} Kr");
         Console.WriteLine($"Ränta: {account._interestRate} %");
         Console.WriteLine("**************************");
      }

   }
}
