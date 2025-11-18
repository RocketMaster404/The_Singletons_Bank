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
       
        public static User LogIn()
        {
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
               
                if (user.Logincheck(passWord,userName) )
                {
                    Console.WriteLine("Lyckad inloggning");
                    return user;
                }
                
                    Console.WriteLine("Misslyckad inloggning");
                
                
            }

            return null;
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
