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
            double epinVal = TransactionManager.GetUserTransactions(EpinData.userID);
            double totalPoints = EpinData.TotalPoints;
            int NoofPins = EpinData.NoOfPins;
            List<EpinModel> lstEpin = null;
            if (totalPoints>=(epinVal*NoofPins))
            {
                lstEpin = new List<EpinModel>();
                for (int i = 0; i < NoofPins; i++)
                {
                    EpinModel Ep = new EpinModel();
                    Guid Ev = new Guid();

                    Ep.CreaterID = EpinData.userID;
                    Ep.VoucherCode = Ev.ToString();

                    lstEpin.Add(Ep);
                }
                return FinancialDataAccess.CreateEpins(lstEpin);
            }
            else
            {
                return -1;
            }


        }
    }
}
