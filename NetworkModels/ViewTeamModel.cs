using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModels
{
    public class ViewTeamModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string ImageExt { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
    }

    public class TeamMember
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string ImageExt { get; set; }
    }
}
