using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDataAccess
{
    class BusinessDataAccess
    {
        private static NetworkMarketingDataContext db = GetDataAccess.GetDataContext();
    }
}
