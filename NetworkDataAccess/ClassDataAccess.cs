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
        public static List<ViewClassUser> GetClassDetailsForViewClass(int ClassID)
        {
            List<ViewClassUser> retVal = new List<ViewClassUser>();
            try
            {
                var ClassDetails = db.Classes.Where(x => x.ClassID == ClassID).FirstOrDefault();
                if (ClassDetails != null)
                {
                    foreach (var item in ClassDetails.ClassUsers)
                    {
                        ViewClassUser vcu = new ViewClassUser()
                        {
                            UserID = item.User.UserID,
                            FirstName = item.User.FirstName,
                            LastName = item.User.LastName,
                            UserName = item.User.Username,
                            ImageExt = item.User.ImageExt,
                            FollowerCount = item.User.LeaderFollowers.Where(x => x.LeaderClassID == ClassDetails.ClassID).Count(),
                            Followers = item.User.LeaderFollowers.Where(x => x.LeaderClassID == ClassDetails.ClassID).Select(x => new ViewClassFollower() { 
                                Name = x.Follower.FirstName + " " + x.Follower.LastName,
                                UserID = x.Follower.UserID,
                                ImageExt = x.Follower.ImageExt
                            }).ToList()
                        };

                        retVal.Add(vcu);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public static int[] GetClassHistory(int UserID)
        {
            int[] retVal = new int[10];
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
