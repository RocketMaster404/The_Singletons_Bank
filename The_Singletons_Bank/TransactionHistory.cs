using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class TransactionHistory
    {
        public int transferType {  get; set; } // 0 för intern överföring // 1 för extern överföring // 2 för insättning
        public int senderId { get; set; }
        public string senderName { get; set; }
        public int recipientId { get; set; }
        public string recipientName { get; set; }
        public string senderCurrency { get; set; }
        public string recipientCurrency { get; set; }
        public decimal amount { get; set; }
        public string createdDate { get; set; }
        public Customer AccountThatCreatedTheTransaction { get; set; }

        public TransactionHistory(Customer AccountThatCreatedTheTransaction1, int isAInternalTransfer1, string senderName1,string recipientName1, int senderId1, string senderCurrency1, int recipientId1, string recipientCurrency1, decimal amount1, string createdDate1)
        {
            AccountThatCreatedTheTransaction = AccountThatCreatedTheTransaction1;
            transferType = isAInternalTransfer1;
            senderId = senderId1;
            senderName = senderName1;
            recipientName = recipientName1;
            senderCurrency = senderCurrency1;
            recipientCurrency = recipientCurrency1;
            recipientId = recipientId1;
            amount = amount1;
            createdDate = createdDate1;
        }
    }
}
