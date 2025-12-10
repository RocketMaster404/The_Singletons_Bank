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
            bool runProgram = true;
            Bank.MonthlyInterest();
            Bank.CreateTestUsers();
            Database.AddAllAccounts(Bank.GetUsers());
            Database.UpdateUserList(Bank.GetUsers());

            while (runProgram)
            {
                User? user = null;

                Console.Clear();
                Utilities.AsciiArtPrinter(true);

                Menu.PrintLogInMenu();
                int input = Utilities.GetUserNumberMinMax(1, 2);

                switch (input)
                {
                    case 1:
                        user = Bank.LogIn();
                        if (user == null || user.UserIsBlocked)
                        {
                            Utilities.NoContentMsg();
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
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine($"Inloggad användare {customer.GetUsername()}");
                Menu.PrintCustomerMainMenu();
                loggedIn = Menu.CustomerMainMenuChoice(customer);
            }

        }

        public static void RunAdminProgram(Admin admin)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Menu.PrintAdminMainMenu();
                loggedIn = Menu.AdminMainMenuChoice(admin);

            }
        }
    }
}
