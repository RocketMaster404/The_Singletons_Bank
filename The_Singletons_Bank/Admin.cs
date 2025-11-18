using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Admin:User
    {
        //public bool IsAdmin = true;



        public Admin(string username, string password) : base(username, password)
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
    }
}
