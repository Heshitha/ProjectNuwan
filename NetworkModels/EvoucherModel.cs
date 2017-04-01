using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModels
{
    public class EvoucherModel
    {
        public int EVouchersID { get; set; }
        public string CreaterName { get; set; }
        public string RecieverName { get; set; }
        public string VoucherCode { get; set; }
        public string CreatedDate { get; set; }
        public string UsedDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
