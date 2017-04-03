using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDataAccess
{
    public class GetDataAccess
    {
        private static NetworkMarketingDataContext db;
        public static NetworkMarketingDataContext GetDataContext()
        {
            if (db == null)
            {
                db = new NetworkMarketingDataContext();
            }
            return db;
        }
    }
}
