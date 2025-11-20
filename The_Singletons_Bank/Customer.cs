using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
   internal class Customer : User
   {
      private List<Account> _accounts;
      private List<SavingAccount> _savingAccounts;

      public Customer(string username, string password) : base(username, password)
      {
         _accounts = new List<Account>();
         _savingAccounts = new List<SavingAccount>();
      }

      public List<Account> GetAccountList()
      {
         return _accounts;
      }

      public void AddToAccountList(Account account)
      {
         _accounts.Add(account);
      }

      public void AddToSavingAccountList(SavingAccount account)
      {
         _savingAccounts.Add(account);
      }

      public static void ShowCustomerAccounts(Customer user)
      {
         if (user._accounts.Count == 0)
         {
            Console.WriteLine("Du har inga aktiva konton");
         }

         foreach (var account in user._accounts)
         {
            Account.ShowAccount(account);
         }
      }

      public static void ShowCustomerSavingAccounts(Customer user)
      {
         if (user._savingAccounts.Count == 0)
         {
            Console.WriteLine("Du har inga aktiva sparkonton");
         }

         foreach (var account in user._savingAccounts)
         {
            SavingAccount.ShowSavingAccountInfo(account);
         }
      }

   }
}
