using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDataAccess
{
    public class UserDataAccess
    {
        private static NetworkMarketingDataContext db = new NetworkMarketingDataContext();
        public static int LoginUser(string userName, string password)
        {
            int retVal = 0;
            try
            {
                User usr = db.Users.Where(x => x.Username == userName).FirstOrDefault();
                if (usr != null)
                {
                    if (usr.Password == password)
                    {
                        retVal = usr.UserID;
                    }
                    else
                    {
                        retVal = -1;
                    }
                }
                else
                {
                    retVal = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public static User GetUserDetails(int userID)
        {
            User retVal = new User();
            try
            {
                User usr = db.Users.Where(x => x.UserID == userID).FirstOrDefault();
                if (usr != null)
                {
                    retVal = usr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
    }
}
