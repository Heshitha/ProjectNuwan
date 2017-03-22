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

                                var classUserResult = sponsor.ClassUsers.Where(x => x.Class.IsActive == true && x.Class.ClassBrokenDate != null && x.IsActive == true).FirstOrDefault();

                                if (classUserResult != null)
                                {
                                    var sponsorClass = classUserResult.Class;

                                    LeaderFollower lef = new LeaderFollower()
                                    {
                                        Class = sponsorClass,
                                        Leader = sponsor,
                                        Follower = newUser,
                                        FollowedDate = DateTime.Today
                                    };
                                    db.LeaderFollowers.InsertOnSubmit(lef);

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

                                    evou.IsUsed = true;
                                    evou.UsedDate = DateTime.Now;
                                    evou.User = newUser;
                                    db.SubmitChanges();
                                    retVal = newUser.UserID;

                                    //starting of the class break
                                    if (userPosition == 13)
                                    {
                                        BreakTheClass(cls);

                                    } 
                                }

                                
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

        private static void BreakTheClass(Class cls)
        {
            var result = db.usp_Get_User_List_Order_By_Performance_For_Class(cls.ClassID).ToArray();

            var fu = result[0];
            int firstUserID = fu.UserID.HasValue ? fu.UserID.Value : 0;
            User firstUser = GetUserDetails(firstUserID);

            cls.IsActive = false;
            cls.ClassBrokenDate = DateTime.Now;
            foreach (var item in cls.ClassUsers)
            {
                item.IsActive = false;
            }

            Class leftClass = new Class()
            {
                ClassCreatedDate = DateTime.Now,
                ClassType = cls.ClassType,
                IsActive = true
            };
            db.Classes.InsertOnSubmit(leftClass);

            Class rightClass = new Class()
            {
                ClassCreatedDate = DateTime.Now,
                ClassType = cls.ClassType,
                IsActive = true
            };
            db.Classes.InsertOnSubmit(rightClass);

            User user = GetUserDetails(result[1].UserID.Value);
            ClassUser clsusr = new ClassUser()
            {
                Class = leftClass,
                IsActive = true,
                Position = 1,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[2].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = rightClass,
                IsActive = true,
                Position = 1,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[3].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = leftClass,
                IsActive = true,
                Position = 2,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[4].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = leftClass,
                IsActive = true,
                Position = 3,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[5].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = rightClass,
                IsActive = true,
                Position = 2,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[6].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = rightClass,
                IsActive = true,
                Position = 3,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[7].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = leftClass,
                IsActive = true,
                Position = 4,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[8].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = leftClass,
                IsActive = true,
                Position = 5,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[9].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = leftClass,
                IsActive = true,
                Position = 6,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[10].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = rightClass,
                IsActive = true,
                Position = 4,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[11].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = rightClass,
                IsActive = true,
                Position = 5,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);

            user = GetUserDetails(result[12].UserID.Value);
            clsusr = new ClassUser()
            {
                Class = rightClass,
                IsActive = true,
                Position = 6,
                User = user,
            };
            db.ClassUsers.InsertOnSubmit(clsusr);
            db.SubmitChanges();

            int firstUserSponcerID = firstUser.SponserID.HasValue ? firstUser.SponserID.Value : 0;
            if (firstUserSponcerID == 0)
            {
                Class biClass = new Class()
                {
                    ClassCreatedDate = DateTime.Now,
                    ClassType = 2,
                    IsActive = true
                };
                db.Classes.InsertOnSubmit(biClass);

                ClassUser cuser = new ClassUser()
                {
                    Class = biClass,
                    IsActive = true,
                    Position = 1,
                    User = firstUser
                };
                db.ClassUsers.InsertOnSubmit(cuser);
            }
            else
            {
                var userSponcer = GetUserDetails(firstUserSponcerID);
                var userActiveBusinessClass = userSponcer.ClassUsers.Where(x => x.IsActive == true && x.Class.ClassBrokenDate == null && x.Class.ClassType == 2).FirstOrDefault();

                var userSponcerActiveClass = userActiveBusinessClass != null ? userActiveBusinessClass.Class : new Class();

                if (userActiveBusinessClass != null && userSponcerActiveClass != null && userSponcer.LeaderFollowers.Where(x => x.Class.ClassID == userSponcerActiveClass.ClassID).Count() < 2)
                {
                    LeaderFollower ledfol = new LeaderFollower()
                    {
                        Class = userSponcerActiveClass,
                        FollowedDate = DateTime.Now,
                        Follower = firstUser,
                        Leader = userSponcer
                    };
                    db.LeaderFollowers.InsertOnSubmit(ledfol);
                }
                else
                {
                    bool isFoundSponcer = false;
                    int i = 1;
                    User greatSponcer = userSponcer.Sponser;
                    Class greatClass = new Class();

                    while (!isFoundSponcer && i < 5)
                    {
                        if (greatSponcer != null)
                        {
                            var greatUserClass = greatSponcer.ClassUsers.Where(x => x.IsActive == true && x.Class.ClassBrokenDate == null && x.Class.ClassType == 2).FirstOrDefault();
                            greatClass = greatUserClass != null ? greatUserClass.Class : new Class();
                            if (greatUserClass != null && greatSponcer.LeaderFollowers.Where(x => x.Class.ClassID == greatClass.ClassID).Count() < 2)
                            {
                                isFoundSponcer = true;
                            }
                            else
                            {
                                greatSponcer = greatSponcer.Sponser;
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    if (isFoundSponcer)
                    {
                        LeaderFollower ledfol = new LeaderFollower()
                        {
                            Class = greatClass,
                            FollowedDate = DateTime.Now,
                            Follower = firstUser,
                            Leader = greatSponcer
                        };
                        db.LeaderFollowers.InsertOnSubmit(ledfol);
                    }
                }

                if (userSponcerActiveClass != null)// && userSponcer.LeaderFollowers.Where(x => x.Class == userSponcerActiveClass).Count() < 2)
                {
                    int position = userSponcerActiveClass.ClassUsers.Count + 1;
                    ClassUser cuser = new ClassUser()
                    {
                        Class = userSponcerActiveClass,
                        IsActive = true,
                        Position = position,
                        User = firstUser
                    };
                    db.ClassUsers.InsertOnSubmit(cuser);
                    if (position == 13)
                    {
                        BreakTheClass(userSponcerActiveClass);
                    }
                }
                else
                {
                    bool isFoundSponcer = false;
                    User greatSponcer = userSponcer.Sponser;
                    Class greatClass = new Class();

                    while (!isFoundSponcer)
                    {
                        if (greatSponcer != null)
                        {
                            var greatUserClass = greatSponcer.ClassUsers.Where(x => x.IsActive == true && x.Class.ClassBrokenDate == null && x.Class.ClassType == 2).FirstOrDefault();
                            greatClass = greatUserClass != null ? greatUserClass.Class : null;
                            //greatClass = greatSponcer.ClassUsers.Where(x => x.IsActive == true && x.Class.ClassBrokenDate == null && x.Class.ClassType == 2).FirstOrDefault().Class;
                            if (greatClass == null)
                            {
                                greatSponcer = greatSponcer.Sponser;
                            }
                            else
                            {
                                isFoundSponcer = true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (isFoundSponcer)
                    {
                        int position = greatClass.ClassUsers.Count + 1;
                        ClassUser cuser = new ClassUser()
                        {
                            Class = greatClass,
                            IsActive = true,
                            Position = position,
                            User = firstUser
                        };
                        db.ClassUsers.InsertOnSubmit(cuser);
                        if (position == 13)
                        {
                            BreakTheClass(greatClass);
                        } 
                    }
                }

                
            }
            db.SubmitChanges();
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
