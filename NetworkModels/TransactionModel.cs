using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModels
{
    public class TransactionModel
    {
        public int TransactionID { get; set; }
        public string SenderName { get; set; }
        public string RecieverName { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
    }
}
