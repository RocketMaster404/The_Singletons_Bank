using System;
using System.Buffers;
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

        public static Dictionary<Customer, decimal> Loantickets = new Dictionary<Customer, decimal>();

        public static List<string> cases { get; set; }

        public Admin(string username, string password) : base(username, password)
        {

        }

        public Admin(string username, string password, bool isadmin) : base(username, password, isadmin)
        {

        }

        public static void Sendinvoice(Customer owner, Loan loan)
        {
            string invoice = loan.ShowLoandetails();
            owner._inbox.Add(invoice);

        }

        public static void Sendinvoice(Customer owner, string msg)
        {
            string invoice = msg;
            owner._inbox.Add(invoice);  
        }
        public static void HandleLoanRequest(Customer owner, decimal loanrequest)
        {
            Console.Write("Sätt ränta:");
            decimal loanRequest = loanrequest;
            decimal setInterest = Utilities.GetUserNumberMinMax(1, 100);

            Loan loan = new Loan(owner, setInterest, loanRequest);
            Sendinvoice(owner, loan);
            Loan.PendingLoans.Add(owner, loan);

            //return loan; //ändra från void till Loan ifall det finns behov i framtiden
        }

        public static void CreateUser()
        {
            Console.Clear();
            Console.WriteLine("Ange användarnamn: ");
            string userName = Utilities.GetUserString();
            Console.WriteLine("Ange Lösenord: ");
            string password = Utilities.GetUserString();

        }

      public static void AvBlockeraAnvändare()
      {
         Console.WriteLine("Ange användarnamnet eller numret av kontot du vill avblockera.");

            List<User> _users = Bank.GetUsers();
            Console.WriteLine("Blockerade konton:");
            int i = 0;
            bool wasUserFound = false;
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
            if(i == 0)
            {
                Utilities.startColoring(ConsoleColor.Red);
                Console.WriteLine("Det fanns inget konto att avblockera");
                Thread.Sleep(1500);
                Console.Clear();
                Utilities.stopColoring();
            }
            
            if(i != 0)
            {
                i = 0;
                string userInput = Utilities.GetUserString();
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
                                wasUserFound = true;
                                Thread.Sleep(1500);
                                Console.Clear();
                                Utilities.stopColoring();
                            }
                        }
                    }
                    if (wasUserFound == false)
                    {
                        Utilities.startColoring(ConsoleColor.Red, ConsoleColor.Black);
                        Console.WriteLine("Du måste välja ett konto");
                        Utilities.stopColoring();
                        Thread.Sleep(1500);
                        Console.Clear();
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
                            wasUserFound = true;
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        else
                        {

                        }
                    }
                    if (!wasUserFound)
                    {
                        Utilities.startColoring(ConsoleColor.Red);
                        Console.WriteLine("Användare hittad inte");
                        Utilities.stopColoring();
                        Thread.Sleep(2000);
                        Console.Clear();
                    }

                }
            }
            

        }
    }

}
