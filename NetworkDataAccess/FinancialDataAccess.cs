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

        public static int CreateEpins(List<EpinModel> Epins)
        {
            try
            {
                foreach (var item in Epins)
                {
                    DateTime utcTime = DateTime.UtcNow;
                    var tz = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                    var tzTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tz);

                    EVoucher Ev = new EVoucher()
                    {
                        CreaterID = item.CreaterID,
                        VoucherCode = item.VoucherCode,
                        CreatedDate = tzTime,
                        IsUsed = false
                    };
                    db.EVouchers.InsertOnSubmit(Ev);
                    db.SubmitChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
