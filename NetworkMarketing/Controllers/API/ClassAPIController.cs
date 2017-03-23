using NetworkDataAccess;
using NetworkMarketing.Models;
using NetworkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetworkMarketing.Controllers.API
{
    public class ClassAPIController : ApiController
    {
        [HttpPost]
        public List<int> GetClassHistoty([FromBody]int UserID)
        {
            List<int> retVal = new List<int>();
            try
            {
                return ClassDataAccess.GetClassHistory(UserID).ToList();
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retVal;
        }

        [HttpPost]
        public ViewClassModel GetClassDetailsForViewClass([FromBody]int ClassID)
        {
            ViewClassModel retVal = new ViewClassModel();
            try
            {
                return ClassDataAccess.GetClassDetailsForViewClass(ClassID);
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retVal;
        }
    }
}
