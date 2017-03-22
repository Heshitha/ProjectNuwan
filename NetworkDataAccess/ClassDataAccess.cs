using NetworkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDataAccess
{
    public class ClassDataAccess
    {
        static NetworkMarketingDataContext db = new NetworkMarketingDataContext();
        public static ViewClassModel GetClassDetailsForViewClass(int ClassID)
        {
            ViewClassModel retVal = new ViewClassModel();
            retVal.UserList = new List<ViewClassUser>();
            List<ViewClassUser> UserList = new List<ViewClassUser>();
            try
            {
                var ClassDetails = db.Classes.Where(x => x.ClassID == ClassID).FirstOrDefault();
                if (ClassDetails != null)
                {
                    retVal.ClassID = ClassDetails.ClassID;
                    retVal.ClassType = ClassDetails.ClassType.Value == 1 ? "Economy" : "Business";
                    foreach (var item in ClassDetails.ClassUsers)
                    {
                        ViewClassUser vcu = new ViewClassUser()
                        {
                            UserID = item.User.UserID,
                            FirstName = item.User.FirstName,
                            LastName = item.User.LastName,
                            UserName = item.User.Username,
                            Position = item.Position,
                            ImageExt = item.User.ImageExt,
                            FollowerCount = item.User.LeaderFollowers.Where(x => x.LeaderClassID == ClassDetails.ClassID).Count(),
                            Followers = item.User.LeaderFollowers.Where(x => x.LeaderClassID == ClassDetails.ClassID).Select(x => new ViewClassFollower() { 
                                Name = x.Follower.Username,
                                UserID = x.Follower.UserID,
                                ImageExt = x.Follower.ImageExt
                            }).ToList()
                        };

                        UserList.Add(vcu);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            retVal.UserList = UserList.OrderBy(x => x.Position).ToList();
            return retVal;
        }

        public static int[] GetClassHistory(int UserID)
        {
            int[] retVal = new int[1];
            try
            {
                var result = db.Users.Where(x => x.UserID == UserID).FirstOrDefault();
                if (result != null)
                {
                    var resultList = result.ClassUsers.Select(x => x.ClassID.Value).Distinct().OrderByDescending(x => x);
                    retVal = resultList.ToArray();
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
