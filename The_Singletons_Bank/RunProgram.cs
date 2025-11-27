using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class RunProgram
    {
        public static bool run = true;

        public static void Run()
        {
            List<User> _users = Bank.GetUsers();
            _users[1].UserIsBlocked = true;
            Utilities.AsciiArtPrinter(true);
            Menu.LogInMenuChoice();
        }

        public static void RunCustomerProgram(Customer customer)
        {
            while (run)
            {
                Console.WriteLine($"Inloggad användare {customer.GetUsername()}");
                Menu.PrintCustomerMainMenu();
                Menu.CustomerMainMenuChoice(customer, run);
            }
        }

        public static void RunAdminProgram(Admin admin)
        {
            while (run)
            {
                Menu.PrintAdminMainMenu();
                Menu.AdminMainMenuChoice(admin, run);
            }
        }
    }
}
