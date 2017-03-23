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
    public class TeamAPIController : ApiController
    {
        [HttpPost]
        public List<ViewTeamModel> GetTeamDetails([FromBody]TeamViewerVM tvm)
        {
            List<ViewTeamModel> retVal = new List<ViewTeamModel>();
            try
            {
                DateTime startDate = Convert.ToDateTime(tvm.startDate);
                DateTime endDate = Convert.ToDateTime(tvm.endDate);
                retVal = NetworkBussiness.TeamManager.GetTeamDetails(tvm.UserID, startDate, endDate);
                retVal.OrderBy(x => x.UserID);
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retVal;
        }
    }
}
