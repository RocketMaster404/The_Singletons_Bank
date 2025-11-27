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



      public Admin(string username, string password) : base(username, password)
      {

      }

        public static void Sendinvoice(Customer owner,Loan loan)
        {
            string invoice=loan.ShowLoandetails();
            owner._inbox.Add(invoice);
           
        }
        public static Loan HandleLoanRequest(Customer owner, decimal loanrequest)
        {
            Console.Write("Sätt ränta:");
            decimal loanRequest = loanrequest;
            decimal setInterest = Utilities.GetUserNumber();

            Loan loan = new Loan(owner, setInterest, loanRequest);
            Admin.Sendinvoice(owner,loan);
            Loan.PendingLoans.Add(owner,loan);
            
            return loan;
        }

        public static void CreateUser()
        {
            Console.WriteLine("Ange användarnamn: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Ange Lösenord: ");
            string password = Console.ReadLine();

      }

      public static void CreateUser()
      {
         Console.WriteLine("Ange användarnamn: ");
         string userName = Console.ReadLine();
         Console.WriteLine("Ange Lösenord: ");
         string password = Console.ReadLine();


         //Bank.users.Add(userName, password); //Users bör ej vara publik. Gör metod för att lägga till users i bank-klassen istället

      }

      public static void UnBlockAccount()
      {
         Console.WriteLine("Ange användarnamnet eller nummret av kontot du vill avblockera.");

         List<User> _users = Bank.GetUsers();
            Console.WriteLine("Blockerade konton:");
            foreach (User user in _users)
            {
                  int i = 0;
                  if(user.UserIsBlocked == true)
                  {
                    i++;
                    Console.WriteLine($"Is user blocked? :|{i}|{user.GetUsername()} {user.UserIsBlocked}");
                  }
            }

            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int number))
            {
                // Om svaret är en int
                foreach (User user in _users)
                {
                    int i = 0;
                    if (user.UserIsBlocked == true)
                    {
                        i++;
                        if(i == Convert.ToInt32(userInput))
                        {
                            user.UserIsBlocked = false;
                            Console.WriteLine($"Is user blocked? : {user.GetUsername()} {user.UserIsBlocked}");
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
                        Console.WriteLine($"Is user blocked? : {user.GetUsername()} {user.UserIsBlocked}");
                    }
                }

            }
            
      }
   }

}
