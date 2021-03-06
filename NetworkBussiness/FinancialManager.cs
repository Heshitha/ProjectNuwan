﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkModels;
using NetworkDataAccess;

namespace NetworkBussiness
{
    public class FinancialManager
    {
        public static int SaveBankDetails(BankTransferModel BankDetails)
        {
            DateTime utcTime = DateTime.UtcNow;
            var tz = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            var tzTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tz);
            int retval = FinancialDataAccess.SaveBankDetails(BankDetails);
            if(retval==1)
            {
                TransactionAddModel Tm = new TransactionAddModel()
                {
                    userID = BankDetails.UserID,
                    RecieverName = "FindRichAccounts",
                    Amount = (float)(BankDetails.Amount),
                    Description = "Bank Transfer",
                    TransactionDate = tzTime,
                    TransactionType = "Send",

                };
                int retval2 = TransactionManager.SaveTransaction(Tm);
            }

            return retval;

        }

        public static List<BankTransferModel> GetAllBankDetails()
        {
            return FinancialDataAccess.GetAllBankDetails();
        }

        public static List<BankTransferModel> GetAllBankDetails(string Username)
        {
            return FinancialDataAccess.GetAllBankDetails(Username);
        }

        public static BankTransferModel GetBankDetailById(int ID)
        {
            return FinancialDataAccess.GetBankDetailById(ID);
        }

        public static int uploadProof(string url, int ID)
        {
            return FinancialDataAccess.uploadProof(url,ID);
        }

        public static int GenerateEpins(EpinGenerateModel EpinData)
        {
            DateTime utcTime = DateTime.UtcNow;
            var tz = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            var tzTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tz);

            double epinVal = EpinData.EpinVal;
            double totalPoints = TransactionManager.GetUserTransactions(EpinData.userID);
            int NoofPins = EpinData.NoOfPins;
            List<EpinModel> lstEpin = null;
            if (totalPoints >= (epinVal * NoofPins))
            {
                lstEpin = new List<EpinModel>();
                for (int i = 0; i < NoofPins; i++)
                {
                    EpinModel Ep = new EpinModel();
                    Guid Ev = Guid.NewGuid();

                    Ep.CreaterID = EpinData.userID;
                    Ep.VoucherCode = Ev.ToString();
                    Ep.Epinvalue = EpinData.EpinVal;

                    lstEpin.Add(Ep);
                }

                int retval = FinancialDataAccess.CreateEpins(lstEpin);

                TransactionAddModel Tm = new TransactionAddModel()
                {
                    userID = EpinData.userID,
                    RecieverName = "admin",
                    Amount = (float)(epinVal * NoofPins),
                    Description = "E-Pin Creation",
                    TransactionDate = tzTime,
                    TransactionType = "Send"

                };
                int retval2 = TransactionManager.SaveTransaction(Tm);
                    return 1;

            }
            else
            {
                return -1;
            }
        }

        public static int GenerateAdminEpins(EpinGenerateModel EpinData)
        {
            List<EpinModel> lstEpin = new List<EpinModel>();
            for (int i = 0; i < EpinData.NoOfPins; i++)
            {
                EpinModel Ep = new EpinModel();
                Guid Ev = Guid.NewGuid();

                Ep.CreaterID = EpinData.userID;
                Ep.VoucherCode = Ev.ToString();
                Ep.Epinvalue = EpinData.EpinVal;

                lstEpin.Add(Ep);

            }

            int retval = FinancialDataAccess.CreateEpins(lstEpin);

            return retval;

        }

        public static List<EvoucherModel> GetEvoucherDetails(int userID, string Epin)
        {
            return FinancialDataAccess.GetEvoucherDetails(userID, Epin);
        }

        public static List<EvoucherModel> GetAllEvoucherDetails(int userID, string Epin)
        {
            return FinancialDataAccess.GetAllEvoucherDetails(userID, Epin);
        }
    }
}
