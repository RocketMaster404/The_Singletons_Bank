using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
   internal class SavingAccount : Account
   {
      private double _interestRate { get; set; }

      public SavingAccount(decimal balance):base(balance,"SEK")
      {
         
      }

      public void CreateSavingAccount(User user) // Måste adderas till users listan
      {
         Console.WriteLine($"Skapa sparkonto\n Ränta: {_interestRate}");
         Console.WriteLine("Vill du skapa ett konto? y/n");
         string input = Console.ReadLine().ToLower();

         if(input == "y")
         {
            var savingAccount = new SavingAccount(0);
            Console.WriteLine($"Du har skapat ett sparkonto");
            ShowAccount(savingAccount);
            // Lägg till det till users lista
         }

         

      }

   }
}
