using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class RunProgram
    {

        public static void Run()
        {
            Utilities.AsciiArtPrinter(true);
            Menu.PrintLogInMenu();
            Menu.LogInMenuChoice();
            

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
