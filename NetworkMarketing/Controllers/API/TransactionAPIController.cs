using NetworkBussiness;
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
    public class TransactionAPIController : ApiController
    {


        [HttpPost]
        // POST: api/TransactionAPI
        public List<TransactionModel> GetAllTransactionsByUser([FromBody]int userID)
        {
            List<TransactionModel> retData = new List<TransactionModel>();
            var result = TransactionManager.GetAllTransactionsByUser(userID);
            if (result != null)
            {
                foreach (var item in result)
                {
                    TransactionModel TranseModel = new TransactionModel();
                    TranseModel.TransactionID = item.TransactionID;
                    TranseModel.SenderName = item.SenderName;
                    TranseModel.RecieverName = item.RecieverName;
                    TranseModel.Amount = (float)item.Amount;
                    TranseModel.Description = item.RecieverName;
                    TranseModel.TransactionDate = Convert.ToDateTime(item.TransactionDate);
                    TranseModel.TransactionType = item.TransactionType;

                    retData.Add(TranseModel);
                }
            }
            return retData;
        }

        [HttpPost]
        public bool CheckTransactionKey([FromBody]TransactionKeyVM TransactionKeyData)
        {
            bool retval = TransactionManager.CheckTransactionKey(TransactionKeyData.userID, TransactionKeyData.TransctionKey);
            return retval;
        }

        [HttpPost]
        public double GetUserTransactions([FromBody]int userID)
        {
            double retval = TransactionManager.GetUserTransactions(userID);
            return retval;
        }

        [HttpPost]
        public int SaveTransaction([FromBody]TransactionAddModel trans)
        {
            int retval = TransactionManager.SaveTransaction(trans);
            return retval;
        }

        // PUT: api/TransactionAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TransactionAPI/5
        public void Delete(int id)
        {
        }
    }
}
