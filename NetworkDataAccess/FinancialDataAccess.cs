using NetworkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDataAccess
{
    public class FinancialDataAccess
    {
        private static NetworkMarketingDataContext db = new NetworkMarketingDataContext();
        public static int SaveBankDetails(BankTransferModel BankDetails)
        {
            //int retVal = 0;

            try
            {
                BankDetail bnkd = new BankDetail()
                {
                    UserID = BankDetails.UserID,
                    TransferType = BankDetails.TransferType,
                    AccType = BankDetails.AccType,
                    AccountName = BankDetails.AccountName,
                    AccountNumber = BankDetails.AccountNumber
                };
                db.BankDetails.InsertOnSubmit(bnkd);
                db.SubmitChanges();

                return 1;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
