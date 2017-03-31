using NetworkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDataAccess
{
    public class TeamDataAccess
    {
        static NetworkMarketingDataContext db = GetDataAccess.GetDataContext();

        public static List<ViewTeamModel> GetTeamDetails(int userID, DateTime startDate, DateTime endDate)
        {
            List<ViewTeamModel> retVal = new List<ViewTeamModel>();
            try
            {
                User user = db.Users.Where(x => x.UserID == userID).FirstOrDefault();
                if (user != null)
                {
                    ViewTeamModel mainObj = new ViewTeamModel()
                    {
                        UserID = user.UserID,
                        UserName = "Direct",
                        ImageExt = user.ImageExt,
                        TeamMembers = user.Followers.Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate).Select(x => new TeamMember()
                        {
                            ImageExt = x.ImageExt,
                            UserID = x.UserID,
                            UserName = x.Username
                        }).ToList()
                    };

                    retVal.Add(mainObj);
                    GetTeamMembers(mainObj, retVal, startDate, endDate);
                    //foreach (var item in mainObj.TeamMembers)
                    //{
                    //    user = db.Users.Where(x => x.UserID == item.UserID).FirstOrDefault();
                    //    ViewTeamModel obj = new ViewTeamModel()
                    //    {
                    //        UserID = user.UserID,
                    //        UserName = user.Username,
                    //        ImageExt = user.ImageExt,
                    //        TeamMembers = user.Followers.Where(x => x.CreatedDate <= endDate).Select(x => new TeamMember()
                    //        {
                    //            ImageExt = x.ImageExt,
                    //            UserID = x.UserID,
                    //            UserName = x.Username
                    //        }).ToList()
                    //    };
                    //    retVal.Add(obj);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public static void GetTeamMembers(ViewTeamModel vtm, List<ViewTeamModel> list, DateTime startDate, DateTime endDate)
        {
            foreach (var item in vtm.TeamMembers)
            {
                User user = db.Users.Where(x => x.UserID == item.UserID).FirstOrDefault();
                ViewTeamModel obj = new ViewTeamModel()
                {
                    UserID = user.UserID,
                    UserName = user.Username,
                    ImageExt = user.ImageExt,
                    TeamMembers = user.Followers.Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate).Select(x => new TeamMember()
                    {
                        ImageExt = x.ImageExt,
                        UserID = x.UserID,
                        UserName = x.Username
                    }).ToList()
                };
                list.Add(obj);
                GetTeamMembers(obj, list, startDate, endDate);
            }
        }
    }
}
