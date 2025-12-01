using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class RunProgram
    {
        private static bool runOnce = true;
      

      public static void Run()
      {
         bool runProgram = true;

         while (runProgram)
         {
            User? user = null;

            
            if (runOnce)
            {
               Utilities.AsciiArtPrinter(runOnce);
               runOnce = false;
            }

            
            
            Menu.PrintLogInMenu();
            int input = Utilities.GetUserNumberMinMax(1, 2);

            switch (input)
            {
               case 1:
                  user = Bank.LogIn();
                  if (user == null || user.UserIsBlocked)
                  {
                     break;
                  }

                  if (user is Customer customer)
                  {
                     RunCustomerProgram(customer);
                  }
                  else if (user is Admin admin)
                  {
                     RunAdminProgram(admin);
                  }
                  break;

               case 2:
                  Console.WriteLine("Programmet avslutas!");
                  runProgram = false;
                  break;
            }
         }
      }


      public static void RunCustomerProgram(Customer customer)
      {
         Console.WriteLine($"Inloggad användare {customer.GetUsername()}");
         Menu.PrintCustomerMainMenu();
         Menu.CustomerMainMenuChoice(customer);
      }

      public static void RunAdminProgram(Admin admin)
      {
         Menu.PrintAdminMainMenu();
         Menu.AdminMainMenuChoice(admin);
      }
    }
}
