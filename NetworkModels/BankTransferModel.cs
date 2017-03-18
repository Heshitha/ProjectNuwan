using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModels
{
    public class BankTransferModel
    {
        public int UserID { get; set; }
        public string TransferType { get; set; }
        public string AccType { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
    }
}
