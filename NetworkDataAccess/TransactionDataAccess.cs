﻿using NetworkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDataAccess
{
    public class TransactionDataAccess
    {
        private static NetworkMarketingDataContext db = new NetworkMarketingDataContext();

        public static List<TransactionModel> GetAllTransactionsByUser(int userID)
        {
            List<TransactionModel> retData = new List<TransactionModel>();

            try
            {
                var AllTrans = db.uspGet_All_TransActionsByUser(userID);
                if(AllTrans!=null)
                {
                    foreach (var item in AllTrans)
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

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return retData;

        }

        public static bool CheckTransactionKey(int userID, string TransctionKey)
        {
            bool retval = false;
            User usr = db.Users.Where(x => x.UserID == userID).SingleOrDefault();
            if(usr!=null)
            {
                if(usr.TransctionKey==TransctionKey)
                {
                    retval = true;
                }
                else
                {
                    retval =false;
                }
            }

            return retval;
        }

    }
}