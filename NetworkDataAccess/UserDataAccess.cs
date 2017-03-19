using NetworkModels;
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

        public static int SignUpUser(SignUpModel signUp)
        {
            int retVal = 0;
            try
            {
                User sponsor = db.Users.Where(x => x.Username == signUp.SponserUserName).FirstOrDefault();
                if (sponsor != null)
                {
                    Class cls = db.Classes.Where(x => x.ClassID == signUp.ClassID && x.IsActive == true).FirstOrDefault();
                    if (cls != null)
                    {
                        if (!db.Users.Any(x => x.Username == signUp.Username))
                        {
                            EVoucher evou = db.EVouchers.Where(x => x.VoucherCode == signUp.EVoucher && x.IsUsed != true).FirstOrDefault();
                            if (evou != null)
                            {
                                User newUser = new User()   
                                {
                                    Username = signUp.Username,
                                    Title = signUp.Title,
                                    FirstName = signUp.FirstName,
                                    LastName = signUp.LastName,
                                    Address = signUp.Address,
                                    Email = signUp.Email,
                                    Password = signUp.Password,
                                    TransctionKey = signUp.TransactionKey,
                                    Country = signUp.Country,
                                    District = signUp.District,
                                    Mobile = signUp.Mobile,
                                    Telephone = signUp.Telephone,
                                    CreatedDate = DateTime.Now,
                                    Sponser = sponsor
                                };
                                db.Users.InsertOnSubmit(newUser);

                                int? nullPosition = cls.ClassUsers.Select(x => x.Position).Max();
                                int userPosition = nullPosition.HasValue ? nullPosition.Value + 1 : 1;

                                if (userPosition <= 13)
                                {
                                    ClassUser clsUsr = new ClassUser()
                                    {
                                        Class = cls,
                                        IsActive = true,
                                        Position = userPosition,
                                        User = newUser,
                                    };
                                    db.ClassUsers.InsertOnSubmit(clsUsr);
                                }

                                //starting of the class break
                                if (userPosition == 13)
                                {
                                    //var classMemberList = from x in cls.ClassUsers
                                    //                      where x.User.Followers
                                    //                      orderby x.User.Followers
                                }

                                evou.IsUsed = true;
                                evou.User = newUser;
                                db.SubmitChanges();
                                retVal = newUser.UserID;
                            }
                            else
                            {
                                //Evoucher invalid
                                retVal = -4;
                            }
                        }
                        else
                        {
                            //Username already exists
                            retVal = -3;
                        }
                    }
                    else
                    {
                        //class not available
                        retVal = -2;
                    }
                }
                else
                {
                    //sponsor is null
                    retVal = -1;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public static bool SaveImageExtension(User user)
        {
            bool retVal = false;
            try
            {
                User usr = db.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
                if (usr != null)
                {
                    usr.ImageExt = user.ImageExt;
                    db.SubmitChanges();
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public static bool ChangePassword(User user)
        {
            bool retVal = false;
            try
            {
                User usr = db.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
                if (usr != null)
                {
                    usr.Password = user.Password;
                    db.SubmitChanges();
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public static bool UpdateUserDetails(User user)
        {
            bool retVal = false;
            try
            {
                User usr = db.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
                if (usr != null)
                {
                    usr.Title = user.Title;
                    usr.FirstName = user.FirstName;
                    usr.LastName = user.LastName;
                    usr.Address = user.Address;
                    usr.Country = user.Country;
                    usr.District = user.District;
                    usr.Mobile = user.Mobile;
                    usr.Telephone = user.Telephone;
                    usr.Email = user.Email;
                    db.SubmitChanges();
                    retVal = true;
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
