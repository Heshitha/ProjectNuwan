using System;
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
            return FinancialDataAccess.SaveBankDetails(BankDetails);
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
               // if(retval!=0 && retval2!=0)
                    return 1;

            }
            else
            {
                return -1;
            }


        }
    }
}
