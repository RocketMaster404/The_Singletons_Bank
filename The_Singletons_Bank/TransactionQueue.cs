using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class TransactionQueue
    {
        private static Queue<PendingTransaction> _que = new();

        public static void EnqueueTransaction(PendingTransaction transaction)
        {
            _que.Enqueue(transaction);
        }

        public static void RunQueue()
        {
            

            while(_que.Count > 0)
            {
                _que.Dequeue();
            }
        }
    }
}
