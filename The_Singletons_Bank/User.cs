using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    public abstract class User
    {
        private string _username; //Gjorde username privat

        private string _password { get; set; } 

        public int LoginAttempts { get; set; } = 3;

        public bool UserIsBlocked { get; set; } = false;

        public bool IsAdmin { get; set; } = false;
        

        

        private decimal Totalfunds = 1000m;


        public User(string username, string password)
        {
            _username = username;
            _password = password;
            
        }

        public User(string username, string password, bool isadmin)
        {
            _username = username;
            _password = password;
            IsAdmin = isadmin;
        }

      
       public bool Admincheck(string password, string username)
        {
            if (_password == password && _username == username && IsAdmin==true)
            {
                return true;
            }
            return false;
        }
        public  bool Logincheck(string password, string username)
        {
            
            if (_password == password && _username==username )
            {
                return true;
            }

            return false;
        }
        public string GetUsername() // En metod för att se vad username innehåller
        {
            return _username;
        }                     
        public void ShowBankAccounts()
        {

        }
        public void CreateNewBankAcc()
        {

        }
        public decimal ShowBalance()
        {
            //Console.WriteLine($"Du har {Totalfunds} kr");//Deleted this part in order for loan class to work
            return Totalfunds;
        }
        public void TakeLoan()
        {
           

        }


    }
}
