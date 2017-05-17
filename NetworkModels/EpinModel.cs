using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModels
{
    public class EpinModel
    {
        public int EVoucherID { get; set; }
        public int CreaterID { get; set; }
        public int UsedBy { get; set; }
        public string VoucherCode { get; set; }
        public double Epinvalue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UsedDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
