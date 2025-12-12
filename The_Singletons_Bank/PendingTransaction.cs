using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static The_Singletons_Bank.TransactionHistory;

namespace The_Singletons_Bank
{
    internal class PendingTransaction
    {
        public Customer Sender { get; set; }
        public Customer Reciver { get; set; }
        public int SenderAccountNumber { get; set; }
        public int ReciverAccountNumber { get; set; }
        public decimal Amount { get; set; }

        public PendingTransaction(Customer sender, Customer reciver,int senderAccountNumber,int reciverAccountNumber,decimal amount)
        {
            Sender = sender;
            Reciver = reciver;
            SenderAccountNumber = senderAccountNumber;
            ReciverAccountNumber = reciverAccountNumber;
            Amount = amount;
            
        }



    }
}
