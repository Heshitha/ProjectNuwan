using NetworkDataAccess;
using NetworkModels;
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

        public static int SignUpUser(SignUpModel signUp)
        {
            return UserDataAccess.SignUpUser(signUp);
        }

        public static bool SaveImageExtension(User user)
        {
            return UserDataAccess.SaveImageExtension(user);
        }

        public static bool ChangePassword(User user)
        {
            return UserDataAccess.ChangePassword(user);
        }

        public static bool UpdateUserDetails(User user)
        {
            return UserDataAccess.UpdateUserDetails(user);
        }
    }
}
