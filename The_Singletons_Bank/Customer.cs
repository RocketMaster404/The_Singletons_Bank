using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Customer:User
    {
      List<Account> accounts; 
      List<SavingAccount> savingAccounts; 

      public Customer(string username, string password) :base(username,password)
        {
         accounts = new List<Account>();
         savingAccounts = new List<SavingAccount>();
        }

      
    }
}
