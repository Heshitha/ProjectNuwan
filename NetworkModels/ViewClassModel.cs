using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModels
{
    public class ViewClassModel
    {

    }

    public class ViewClassUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public int FollowerCount { get; set; }
        public List<ViewClassFollower> Followers { get; set; }

        public string LastName { get; set; }

        public string ImageExt { get; set; }
    }

    public class ViewClassFollower
    {
        public int UserID { get; set; }
        public string Name { get; set; }

        public string ImageExt { get; set; }
    }
}
