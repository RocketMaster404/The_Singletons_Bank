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

        public static bool LogInActive;

        public static bool userExists(string username)
        {
            foreach (var user in _users)
            {
                if (user.Username == username)
                {
                    return true;
                }
               
            }
            return false;
        }
        public static User LogIn()
        {
            while (true)
            {
                Console.WriteLine("Ange användarnamn:");
                string userName = Console.ReadLine();
                bool check=userExists(userName);

                if (check == false)
                {
                    Console.WriteLine("Användaren finns inte. Försök igen");
                    continue;
                }
                Console.WriteLine("Ange Lösenord:");
                string passWord = Console.ReadLine();

                foreach (var user in _users)
                {
                    if (user.Admincheck(passWord, userName))
                    {
                        if (user.Username == userName && user.UserIsBlocked == true)
                        {

                            break;
                        }
                        Console.WriteLine("Lyckad Admin inloggning");
                        return user;

                    }
                }

                foreach (var user in _users)
                {
                    if (user.Username == userName && user.UserIsBlocked == true)
                    {
                        Console.WriteLine("Användare blockerad. Var god kontakta administratör");
                        return user;
                    }

                    if (user.Logincheck(passWord, userName))
                    {
                        Console.WriteLine("Lyckad inloggning");
                        return user;
                    }

                    else if (user.Username == userName)
                    {
                        if (user.LoginAttempts == 1)
                        {
                            user.UserIsBlocked = true;
                            Console.WriteLine($"Användare {user.Username} är låst. Var god kontakta administratör");
                            return user;

                        }
                        user.LoginAttempts--;
                        Console.Write($"Fel lösenord för {user.Username}, Du har ");
                        Utilities.startColoring(ConsoleColor.Red,ConsoleColor.Black);
                        Console.Write($"{user.LoginAttempts}");
                        Console.ResetColor();
                        Console.Write(" försök kvar.\n");
                        break;
                    }

                }

            }
            
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
