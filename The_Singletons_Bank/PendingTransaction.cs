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
        public Customer Customer { get; set; }
        public TransferType Type { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public int SenderAccountNumber { get; set; }
        public string Currency { get; set; }
        public int ReicerverAccountNUmber { get; set; }
        public string ReceiverCurrency { get; set; }
        public decimal Amount { get; set; }


        public PendingTransaction(
          Customer customer,
          TransferType type,
          string sender,
          string reciever,
          int senderAccountNumber,
          string currency,
          int recieverAccountnumber,
          string receiverCurrency,
          decimal amount,
          DateTime createdAt)
        {
            Customer = customer;
            Type = type;
            Sender = sender;
            Reciever = reciever;
            SenderAccountNumber = senderAccountNumber;
            Currency = currency;
            ReicerverAccountNUmber = recieverAccountnumber;
            ReceiverCurrency = receiverCurrency;
            Amount = amount;
            
        }
    }
}
