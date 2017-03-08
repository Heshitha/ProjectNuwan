using NetworkDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkBussiness
{
    public class UserManager
    {
        public static int LoginUser(string userName, string password)
        {
            return UserDataAccess.LoginUser(userName, password);
        }

        public static User GetUserDetails(int userID)
        {
            return UserDataAccess.GetUserDetails(userID);
        }
    }
}
