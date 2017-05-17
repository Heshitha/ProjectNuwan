using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModels
{
    public class BankTransferModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string TransferType { get; set; }
        public string AccType { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public float Amount { get; set; }
        public string BankName { get; set; }
        public string Nic { get; set; }
        public string Address { get; set; }
        public string ProofUrl { get; set; }
    }
}
