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
            try
            {
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
                        TranseModel.Description = item.Description;
                        TranseModel.TransactionDate = item.TransactionDate == null ? "N/A" : Convert.ToDateTime(item.TransactionDate).ToString("g");
                        TranseModel.TransactionType = item.TransactionType;

                        retData.Add(TranseModel);
                    }
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retData;
        }

        [HttpPost]
        public bool CheckTransactionKey([FromBody]TransactionKeyVM TransactionKeyData)
        {
            bool retval = false;
            try
            {
                retval = TransactionManager.CheckTransactionKey(TransactionKeyData.userID, TransactionKeyData.TransctionKey);
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retval;
        }

        [HttpPost]
        public double GetUserTransactions([FromBody]int userID)
        {
            double retval = 0.00;
            try
            {
                retval = TransactionManager.GetUserTransactions(userID);
            }
            catch (Exception ex )
            {
                LogClass.WriteErrorLog(ex);
            }
            return retval;
        }

        [HttpPost]
        public int SaveTransaction([FromBody]TransactionAddModel trans)
        {
            int retval = 0;
            try
            {
                retval = TransactionManager.SaveTransaction(trans);
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retval;
        }

        public bool checkUserName([FromBody]TransactionKeyVM trans)
        {
            bool retval = false;
            try
            {
                retval = TransactionManager.checkUserName(trans.userName);
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
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
