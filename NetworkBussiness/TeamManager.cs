using NetworkDataAccess;
using NetworkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkBussiness
{
    public class TeamManager
    {
        public static List<ViewTeamModel> GetTeamDetails(int userID, DateTime startDate, DateTime endDate)
        {
            return TeamDataAccess.GetTeamDetails(userID, startDate, endDate);
        }
    }
}
