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
           new Admin("Admin","4321",true)
        };

        public static List<User> GetUsers()
        {
            return _users;
        }

        public static User LogIn()
      {
            // Temporrary thigny mahjign
            Customer firstCustomer = _users[0] as Customer;
            firstCustomer.UserIsBlocked = true;
            // REMPORTAR
            Console.WriteLine("Ange användarnamn:");
         string userName = Console.ReadLine();
         Console.WriteLine("Ange Lösenord:");
         string passWord = Console.ReadLine();

         foreach (var user in _users)
         {
            if (user.Admincheck(passWord, userName))
            {
               Console.WriteLine("Lyckad Admin inloggning");
               return user;
            }
         }


         foreach (var user in _users)
         {

            if (user.Logincheck(passWord, userName))
            {
               Console.WriteLine("Lyckad inloggning");
               return user;
            }

            Console.WriteLine("Misslyckad inloggning");


         }

         return null;
      }

      public void AddCustomer()
      {
         bool userNameUnique = true;
         Console.WriteLine("Skapa kundkort\n");
         string userName;
         do
         {
            Console.WriteLine("Ange användarnamn: ");
            userName = Console.ReadLine();

            foreach (var user in _users)
            {
               if (user.Username == userName)
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
      public void AddAdminAccount()
      {
         bool userNameUnique = true;
         Console.WriteLine("Skapa Adminkonto\n");
         string userName;
         do
         {
            Console.WriteLine("Ange användarnamn: ");
            userName = Console.ReadLine();

            foreach (var user in _users)
            {
               if (user.Username == userName)
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









      //public static void AddUser(string userName, string password)
      //{
      //    if (userExists)
      //    {
      //        // felmeddelande
      //        return;
      //    }
      //    _users.Add()
      //}


   }
}
