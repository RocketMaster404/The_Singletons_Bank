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

        public static void CreateQuedTransaction(Customer sender, Customer reciver, int senderAccountNumber, int reciverAccountNumber, decimal amount)
        {
            var transaction = new PendingTransaction(sender, reciver, senderAccountNumber, reciverAccountNumber, amount);
            _que.Enqueue(transaction);
        }

        public static void RunQueue()
        {
            while (_que.Count > 0)
            {
                PendingTransaction transaction = _que.Dequeue();

                List<Account> senderLists = transaction.Sender.GetAccountList();
                List<Account> reciverLists = transaction.Reciver.GetAccountList();

                Account sender = null;
                Account reciver = null;

                foreach (var account in senderLists)
                {
                    if (account.GetAccountNumber() == transaction.SenderAccountNumber)
                    {
                        sender = account;
                        break;

                    }
                }

                foreach (var account in reciverLists)
                {
                    if (account.GetAccountNumber() == transaction.ReciverAccountNumber)
                    {
                        reciver = account;
                    }
                }

                if (sender != null && reciver != null)
                {
                    reciver.AddMoney(transaction.Amount);


                }
            }
        }
    }
}
