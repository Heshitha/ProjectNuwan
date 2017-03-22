using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModels
{
    public class EpinGenerateModel
    {
        public int userID { get; set; }
        public double TotalPoints { get; set; }
        public double EpinVal { get; set; }
        public string TransactionKey { get; set; }
        public int NoOfPins { get; set; }
    }
}
