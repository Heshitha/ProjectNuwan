using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkMarketing.Models
{
    public class BankTransferVM
    {
        public int UserID { get; set; }
        public string TransferType { get; set; }
        public string AccType { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
    }
}