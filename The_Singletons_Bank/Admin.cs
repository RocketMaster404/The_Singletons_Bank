using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
   internal class Admin : User
   {
      //public bool IsAdmin = true;
      





      public Admin(string username, string password) : base(username, password)
      {

      }

      public Admin(string username, string password, bool isadmin) : base(username, password, isadmin)
      {

      }

      public static void CreateUser()
      {
         Console.WriteLine("Ange användarnamn: ");
         string userName = Console.ReadLine();
         Console.WriteLine("Ange Lösenord: ");
         string password = Console.ReadLine();


         //Bank.users.Add(userName, password); //Users bör ej vara publik. Gör metod för att lägga till users i bank-klassen istället

      }

      //Simon Code :)

      public static void UnBlockAccount()
      {
         Console.WriteLine("Enter the name of the User you want to unblock");
         string username = Console.ReadLine();
         List<User> _users = Bank.GetUsers();
         //Console.WriteLine(_users[0].UserIsBlocked);

         foreach (User user in _users)
         {
            if (user.GetUsername() == username)
            {
               user.UserIsBlocked = false;
               Console.WriteLine($"Is user blocked? : {user.GetUsername()} {user.UserIsBlocked}");
            }
         }
      }
   }

}
