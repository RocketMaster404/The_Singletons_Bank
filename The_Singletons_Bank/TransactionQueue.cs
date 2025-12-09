using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static The_Singletons_Bank.TransactionHistory;


namespace The_Singletons_Bank
{
    internal class TransactionQueue
    {
        private static Queue<PendingTransaction> _que = new();

        public static void EnqueueTransaction(PendingTransaction transaction)
        {
            _que.Enqueue(transaction);
        }

        public static void CreateQuedTransaction(Customer customer, TransferType type, Account sender, Account reciver, decimal amount)
        {
            var transaction = new PendingTransaction(customer,
        type,
        sender.Name,
        reciver.Name,
        sender.GetAccountNumber(),
        sender.GetCurrency(),
        reciver.GetAccountNumber(),
        reciver.GetCurrency(),
        amount);
            _que.Enqueue(transaction);



        }

        public static void RunQueue()
        {


            while (_que.Count > 0)
            {
                PendingTransaction transaction = _que.Dequeue();

                var accounts = transaction.Customer.GetAccountList();



                Account sender = null;
                foreach (var account in accounts)
                {
                    if (account.GetAccountNumber() == transaction.SenderAccountNumber)
                    {
                        sender = account;
                        break;
                    }
                }



                Account reciver = null;
                foreach (var account in accounts)
                {
                    if (account.GetAccountNumber() == transaction.ReicerverAccountNUmber)
                    {
                        reciver = account;
                        break;
                    }
                }

                if (sender != null && reciver != null)
                {
                    //sender.RemoveMoney(transaction.Amount);
                    reciver.AddMoney(transaction.Amount);

                    // Historik
                }

            }
        }
    }
}
