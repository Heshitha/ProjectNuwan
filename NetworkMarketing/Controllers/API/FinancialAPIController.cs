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
            int retval = FinancialManager.SaveBankDetails(BankDetails);
            return retval;
        }

        [HttpPost]
        public int GenerateEpins([FromBody]EpinGenerateModel EpinModel)
        {
            int retval = FinancialManager.GenerateEpins(EpinModel);
            return retval;
        }
    }
}