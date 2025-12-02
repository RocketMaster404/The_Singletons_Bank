using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace The_Singletons_Bank
{
    internal class Admin : User
    {
        //public bool IsAdmin = true;


        public static Dictionary<Customer, decimal> Loantickets = new Dictionary<Customer, decimal>();

        public static List<string> cases { get; set; }



        public Admin(string username, string password) : base(username, password)
        {

        }

        public Admin(string username, string password, bool isADmin) : base(username, password)
        {

        }



        public static void Sendinvoice(Customer owner, Loan loan)
        {
            string invoice = loan.ShowLoandetails();
            owner._inbox.Add(invoice);

        }
        public static Loan HandleLoanRequest(Customer owner, decimal loanrequest)
        {
            Console.Write("Sätt ränta:");
            decimal loanRequest = loanrequest;
            decimal setInterest = Utilities.GetUserNumberMinMax(1, 100);

            Loan loan = new Loan(owner, setInterest, loanRequest);
            Admin.Sendinvoice(owner, loan);
            Loan.PendingLoans.Add(owner, loan);

            return loan;
        }

        public static void CreateUser()
        {
            Console.Clear();
            Console.WriteLine("Ange användarnamn: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Ange Lösenord: ");
            string password = Console.ReadLine();

        }


        public static void UnBlockAccount()
        {
            Console.WriteLine("Ange användarnamnet eller numret av kontot du vill avblockera.");

            List<User> _users = Bank.GetUsers();
            Console.WriteLine("Blockerade konton:");
            int i = 0;
            foreach (User user in _users)
            {

                if (user.UserIsBlocked == true)
                {
                    i++;
                    Utilities.startColoring(ConsoleColor.Red, ConsoleColor.Black);
                    Console.WriteLine($"användare :|{i}|{user.GetUsername()}");
                    Utilities.stopColoring();
                }
            }
            i = 0;
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int number))
            {
                // Om svaret är en int
                foreach (User user in _users)
                {

                    if (user.UserIsBlocked == true)
                    {
                        i++;
                        if (i == Convert.ToInt32(userInput))
                        {
                            user.UserIsBlocked = false;
                            user.LoginAttempts = 3;
                            Utilities.startColoring(ConsoleColor.Green, ConsoleColor.Black);
                            Console.WriteLine($"Användaren : {user.GetUsername()} har nu tillgång till bankens system igen");
                            Thread.Sleep(2000);
                            Console.Clear();
                            Utilities.stopColoring();
                        }
                    }
                }
            }
            else
            {
                // Om svaret är en string
                foreach (User user in _users)
                {
                    if (user.GetUsername() == userInput)
                    {
                        user.UserIsBlocked = false;
                        Utilities.startColoring(ConsoleColor.Green, ConsoleColor.Black);
                        Console.WriteLine($"Användaren : {user.GetUsername()} har nu tillgång till bankens system igen");
                        Utilities.stopColoring();
                    }
                }

            }

        }
    }

}
