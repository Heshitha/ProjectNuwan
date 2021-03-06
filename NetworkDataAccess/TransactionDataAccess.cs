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
        private static NetworkMarketingDataContext db = GetDataAccess.GetDataContext();

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
                        TranseModel.Description = item.Description;
                        TranseModel.TransactionDate = item.TransactionDate==null?"N/A": Convert.ToDateTime(item.TransactionDate).ToString("g");
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

        public static bool checkUserName(string userName)
        {
            bool retval = false;
            try
            {
                User usr = db.Users.Where(x => x.Username == userName).SingleOrDefault();
                if (usr != null)
                {
                    if (usr.Username == userName)
                    {
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return retval;
        }

        public static int SaveTransaction(TransactionAddModel trans)
        {
            int retval = 0;
            try
            {
                DateTime utcTime = DateTime.UtcNow;
                var tz = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                var tzTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tz);
                var retval1 = db.usp_Add_New_Transaction((int)trans.userID, trans.RecieverName, (double)trans.Amount, trans.Description, tzTime,trans.TransactionType);
                retval =1;
            }
            catch (Exception ex)
            {
                retval= 0;
                throw ex;
            }
            return retval;
        }

        public static double GetUserTransactions(int userID)
        {
            double? Allpoints = 0;
            var retData = db.usp_Get_User_points(userID,ref Allpoints);
            return Allpoints.HasValue ? Allpoints.Value : 0.00;
        }

        public static bool CheckTransactionKey(int userID, string TransctionKey)
        {
            bool retval = false;
            try
            {
                User usr = db.Users.Where(x => x.UserID == userID).SingleOrDefault();
                if (usr != null)
                {
                    if (usr.TransctionKey == TransctionKey)
                    {
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retval;
        }

    }
}
