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
      private static List<User> _users = new List<User>();
       

      public static List<User> GetUsers()
      {
         return _users;
      }

      public static void CreateTestUsers()
      {
         
         var olof = new Customer("olof", "1234");
         olof.AddToAccountList(new Account("Lönekonto", 5000, "SEK"));
         olof.AddToAccountList(new Account("Sparkonto", 12000, "SEK"));
         olof.AddToAccountList(new Account("USA konto", 120, "USD"));
         olof.AddToSavingAccountList(new SavingAccount("Standard Spar", 100, 1.5m));
         olof.AddToSavingAccountList(new SavingAccount("Medel Spar", 100, 1.7m));
         olof.AddToSavingAccountList(new SavingAccount("Långsiktigt Spar", 100, 2.0m));
         _users.Add(olof);
        
         var stig = new Customer("stig", "2345");
         stig.AddToAccountList(new Account("Konto 1", 2000, "SEK"));
         _users.Add(stig);

         var admin = new Admin("Admin", "4321", true);
         _users.Add(admin);

        
      }


      public static Customer GetSpecificUser(string username)
      {
         foreach (var user in _users)
         {
            if (user.GetUsername() == username && user is Customer customer)
            {
               return customer;
            }
         }
         return null;
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
                  Console.ReadLine();
                  return null;
               }

               if (user.Logincheck(passWord, userName))
               {
                  Console.WriteLine("Lyckad inloggning");
                  Thread.Sleep(1000);
                  Console.Clear();

                  return user;
               }

               else if (user.GetUsername() == userName && user.IsAdmin == false)
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
         Utilities.NoContentMsg();
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
         Utilities.NoContentMsg();

        }

      public static void MonthlyInterest()
      {


         if (DateTime.Now.Day == 1 && DateTime.Now.Hour == 12 && DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
         {
            foreach (var user in _users)
            {
               if (user is Customer customer)
               {
                  foreach (var account in customer.GetSavingAccountList())
                  {
                     account.IncreaseBalanceInterestRate();
                  }
               }
            }
         }
      }


   }
}
