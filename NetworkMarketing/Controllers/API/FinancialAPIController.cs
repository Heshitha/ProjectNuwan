using NetworkMarketing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using NetworkBussiness;
using NetworkModels;

namespace NetworkMarketing.Controllers.API
{
    public class FinancialAPIController : ApiController
    {
        [HttpPost]
        public int SaveBankDetails([FromBody]BankTransferModel BankDetails)
        {
            int retval = 0;
            try
            {
                retval = FinancialManager.SaveBankDetails(BankDetails);
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retval;
        }

        [HttpPost]
        public int GenerateEpins([FromBody]EpinGenerateModel EpinModel)
        {
            int retval = 0;
            try
            {
                retval = FinancialManager.GenerateEpins(EpinModel);
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retval;
        }
        [HttpPost]
        public List<EvoucherModel> GetEvoucherDetails([FromBody]EvoucherGetModel EV)
        {
            try
            {
                return FinancialManager.GetEvoucherDetails(Convert.ToInt32(EV.userID), EV.Epin);
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
                return null;
            }
        }

        [HttpPost]
        public List<EvoucherModel> GetALLEvoucherDetails([FromBody]EvoucherGetModel EV)
        { 
            try
            {
                return FinancialManager.GetAllEvoucherDetails(Convert.ToInt32(EV.userID), EV.Epin);
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
                return null;
            }
        }
    }
}