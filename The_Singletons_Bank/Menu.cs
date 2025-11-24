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
               while (user != null)
               {


                  if (user != null && user.UserIsBlocked != true && user is Customer customer)//LogIn-logiken följer med hit
                  {
                     Console.WriteLine($"Inloggad användare {customer.GetUsername()}");
                     PrintCustomerMainMenu();
                     CustomerMainMenuChoice(customer);
                  }
                  //else if()
                  //{
                  //   PrintAdminMainMenu();
                  //   AdminMainMenuChoice();
                  //}
                  else
                  {

                     RunProgram.Run();
                  }
                  Console.ReadKey();
               }
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

      public static void CustomerMainMenuChoice(Customer user)
      {
         int input = Utilities.GetUserNumberMinMax(1, 5);
         switch (input)
         {
            case 1:
               Customer.ShowCustomerAccounts(user);
               Customer.ShowCustomerSavingAccounts(user);
               Console.ReadKey();
               break;
            case 2:
               Console.WriteLine("Överföring");
               PrintTransferMenu();
               TransferMenuChoice(user);
               break;
            case 3:
               PrintCreateAccountMenu();
               CreateAccountMenuChoice(user);
               break;
            case 4:
                    Loan.ShowLoanMenu(user);
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
         Console.WriteLine("3. UnBlockAccount"); // I have added this choise [Simon, 2025-11-19]
         Console.WriteLine("4. Logga ut");
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
            case 3: // I have added this case and functon for unlocking accounts [Simon, 2025-11-19]
               Console.WriteLine("3. UnBlockAccount");
               Admin.UnBlockAccount();

               break;
            case 4:
               Console.WriteLine("4. Logga ut");
               break;
         }


      }

        public static void PrintTransferMenu()
        {
            Console.WriteLine("1. Internal Överföring");
            Console.WriteLine("2. External Överföring");
        }
        public static void TransferMenuChoice(Customer user)
        {
            int input = Utilities.GetUserNumberMinMax(1, 2);
            switch (input)
            {
                case 1:
                    Transaction.InternalTransfer(user);
                    break;
                case 2:
                    Transaction.ExternalTransfer(user);
                    break;
            }

        }

        public static void PrintCreateAccountMenu()
      {
         Console.WriteLine("1. Skapa konto");
         Console.WriteLine("2. Skapa sparkonto");
      }
      public  static void CreateAccountMenuChoice(Customer user)
      {
         int input = Utilities.GetUserNumberMinMax(1, 2);
         switch (input)
         {
            case 1:
               Account.CreateAccount(user);
               break;
            case 2:
               SavingAccount.CreateSavingAccount(user);
               break;
         }

      }







   }
}
