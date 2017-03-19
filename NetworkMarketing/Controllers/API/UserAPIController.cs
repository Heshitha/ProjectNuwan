using NetworkBussiness;
using NetworkDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NetworkMarketing.Models;

namespace NetworkMarketing.Controllers.API
{
    public class UserAPIController : ApiController
    {
        [HttpPost]
        public UserVM GetUserDetails([FromBody]int userID)
        {
            UserVM retVal = new UserVM();
            try
            {
                var result = UserManager.GetUserDetails(userID);
                if (result != null)
                {
                    retVal = new UserVM()
                    {
                        UserID = result.UserID,
                        Title = result.Title,
                        Username = result.Username,
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        Address = result.Address,
                        Country = result.Country,
                        District = result.District,
                        Mobile = result.Mobile,
                        Telephone = result.Telephone,
                        Email = result.Email,
                        ImageExt = result.ImageExt
                    };
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retVal;
        }

        [HttpPost]
        public bool ChangePassword([FromBody]UserVM user)
        {
            bool retVal = false;
            try
            {
                NetworkDataAccess.User usr = UserManager.GetUserDetails(user.UserID);
                if (user.NewPassword == user.ConfirmPassword && usr.Password == user.CurrentPassword)
                {
                    retVal = UserManager.ChangePassword(new NetworkDataAccess.User()
                    {
                        UserID = user.UserID,
                        Password = user.NewPassword
                    });
                }
                else
                {
                    retVal = false;
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retVal;
        }

        [HttpPost]
        public bool UpdateUserDetails([FromBody]UserVM user)
        {
            bool retVal = false;
            try
            {
                NetworkDataAccess.User usr = UserManager.GetUserDetails(user.UserID);

                retVal = UserManager.UpdateUserDetails(new NetworkDataAccess.User()
                {
                    UserID = user.UserID,
                    Title = user.Title,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    Country = user.Country,
                    District = user.District,
                    Mobile = user.Mobile,
                    Telephone = user.Telephone,
                    Email = user.Email
                });
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retVal;
        }
    }
}
