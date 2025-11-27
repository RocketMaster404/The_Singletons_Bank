using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
   internal class Bank
   {
      private static List<User> _users = new List<User>()
        {
            new Customer("olof", "1234"),
            new Customer("stig", "2345"),
            new Admin("Admin","4321",true)
        };

      public static List<User> GetUsers()
      {
         return _users;
      }

      public static bool userExists(string username)
      {
         foreach (var user in _users)
         {
            if (user.GetUsername() == username)
            {
               return true;
            }
         }
         return false;
      }

      public static User? LogIn()
      {
         User? currentUser = null;
         while (currentUser == null)
         {
            Console.WriteLine("Ange användarnamn:");
            string userName = Console.ReadLine();
            bool check = userExists(userName);

            if (check == false)
            {
               Console.WriteLine("Användaren finns inte. Försök igen");
               continue;
            }

            Console.WriteLine("Ange Lösenord:");
            string passWord = Console.ReadLine();

            foreach (var user in _users)
            {
               if (user.GetUsername() == userName && user.UserIsBlocked == true)
               {
                  Console.WriteLine("Användare blockerad. Var god kontakta administratör");
                  return null;
               }

               if (user.Logincheck(passWord, userName))
               {
                  Console.WriteLine("Lyckad inloggning");
                  
                  return user;
               }

               else if (user.GetUsername() == userName)
               {
                  if (user.LoginAttempts == 1)
                  {
                     user.UserIsBlocked = true;
                     Console.WriteLine($"Användare {user.GetUsername()} är låst. Var god kontakta administratör");
                     return null;
                  }

                  user.LoginAttempts--;
                  Console.Write($"Fel lösenord för {user.GetUsername()}, Du har ");
                  Utilities.startColoring(ConsoleColor.Red, ConsoleColor.Black);
                  Console.Write($"{user.LoginAttempts}");
                  Console.ResetColor();
                  Console.Write(" försök kvar.\n");
                  break;
               }
            }
         }
         return currentUser;
      }

      public static void AddCustomer()
      {
         bool userNameUnique = true;
         Console.WriteLine("Skapa kundkort\n");
         string userName;

         do
         {
            Console.WriteLine("Ange användarnamn: ");
            userName = Console.ReadLine();
            userNameUnique = true;

            foreach (var user in _users)
            {
               if (user.GetUsername() == userName)
               {
                  Console.WriteLine("Användarnamn upptaget");
                  userNameUnique = false;
               }
            }

         } while (!userNameUnique);

         Console.WriteLine("Ange användarens lösenord: ");
         string password = Console.ReadLine();

         var customer = new Customer(userName, password);
         _users.Add(customer);
         Console.WriteLine($"Användare: {userName} har skapats");
      }

      public static void AddAdminAccount()
      {
         bool userNameUnique = true;
         Console.WriteLine("Skapa Adminkonto\n");
         string userName;

         do
         {
            Console.WriteLine("Ange användarnamn: ");
            userName = Console.ReadLine();
            userNameUnique = true;

            

            foreach (var user in _users)
            {
               if (user.GetUsername() == userName)
               {
                  Console.WriteLine("Användarnamn upptaget");
                  userNameUnique = false;
               }
            }

            foreach (var user in _users)
            {
               if (user.GetUsername() == userName)
               {
                  Console.WriteLine("Användarnamn upptaget");
                  userNameUnique = false;
               }
            }

         } while (!userNameUnique);

         Console.WriteLine("Ange användarens lösenord: ");
         string password = Console.ReadLine();

         var admin = new Admin(userName, password, true);
         _users.Add(admin);
         Console.WriteLine($"Admin: {userName} har skapats");

        
      }


   }
}
