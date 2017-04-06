using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkMarketing.Models
{
    public class TransactionKeyVM
    {
        public int userID { get; set; }
        public string TransctionKey { get; set; }
        public string userName { get; set; }
    }
}