using NetworkDataAccess;
using NetworkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkBussiness
{
    public class ClassManager
    {
        public static List<ViewClassUser> GetClassDetailsForViewClass(int ClassID)
        {
            return ClassDataAccess.GetClassDetailsForViewClass(ClassID);
        }

        public static int[] GetClassHistory(int UserID)
        {
            return ClassDataAccess.GetClassHistory(UserID);
        }
    }
}
