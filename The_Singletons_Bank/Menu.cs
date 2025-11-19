using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
   internal class Menu
   {

     

      public static void PrintLogInMenu()
      {
         Console.WriteLine("Singletons Bank - since 1807\n");
         Console.WriteLine("1. Logga in");
         Console.WriteLine("2. Avsluta");
         Console.Write("Ange val: ");
      }

      public static void LogInMenuChoice()
      {

         int input = Utilities.GetUserNumberMinMax(1, 2);
         switch (input)
         {
            case 1:
                    var user = Bank.LogIn();
              
               if(user!=null&&user.UserIsBlocked!=true)
               {
                  PrintCustomerMainMenu();
                  CustomerMainMenuChoice();
               }
               //else if()
               //{
               //   PrintAdminMainMenu();
               //   AdminMainMenuChoice();
               //}
               
                  break;
            case 2:
               Console.WriteLine("Avsluta");
               break;

               

         }

      }
      public static void PrintCustomerMainMenu()
      {
         Console.WriteLine("Meny");
         Console.WriteLine("1. Kontoöversikt"); // Undermeny (Transaktions historik)
         Console.WriteLine("2. Överföring"); // gör undermeny
         Console.WriteLine("3. Skapa konto"); // Gör undermeny
         Console.WriteLine("4. Lån"); // gör under meny
         Console.WriteLine("5. Logga ut");
         Console.Write("Ange val: ");
      }

      public static void CustomerMainMenuChoice()
      {
         int input = Utilities.GetUserNumberMinMax(1, 5);
         switch (input)
         {
            case 1:
               Console.WriteLine("Kontoöversikt");
               break;
            case 2:
               Console.WriteLine("Överföring");
               break;
            case 3:
               Console.WriteLine("Skapa konto");
               break;
            case 4:
               Console.WriteLine("Lån");
               break;
            case 5:
               Console.WriteLine("Logga ut");
               break;
         }

      }

      public static void PrintAdminMainMenu()
      {
         Console.WriteLine("Admin Meny\n");
         Console.WriteLine("1. Skapa användare");
         Console.WriteLine("2. Växelkurs");
         Console.WriteLine("3. Logga ut");
      }

      public static void AdminMainMenuChoice()
      {
         int input = Utilities.GetUserNumberMinMax(1, 3);

         switch (input)
         {
            case 1:
               Console.WriteLine("1. Skapa användare");
               break;
            case 2:
               Console.WriteLine("2. Växelkurs");
               break;
            case 3:
               Console.WriteLine("3. Logga ut");
               break;
         }

         
      }







   }
}
