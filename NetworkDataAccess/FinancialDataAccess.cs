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
        private static NetworkMarketingDataContext db = GetDataAccess.GetDataContext();
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
            int retval = 0;
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
                retval= 1;
            }
            catch (Exception ex)
            {
                retval= 0;
                throw ex;
            }

            return retval;
        }

        public static List<EvoucherModel> GetEvoucherDetails(int userID, string Epin)
        {
            List<EvoucherModel> retData = new List<EvoucherModel>();
            try
            {
                var AllEvouchers = db.usp_Get_Evoucher_Details(Epin,userID);
                if(AllEvouchers!=null)
                {
                    foreach (var item in AllEvouchers)
                    {
                        EvoucherModel EM = new EvoucherModel();
                        EM.EVouchersID = Convert.ToInt32(item.EVouchersID);
                        EM.CreaterName = item.Username;
                        EM.RecieverName = item.RecievedBy;
                        EM.VoucherCode = item.VoucherCode;
                        EM.CreatedDate = item.CreatedDate == null?"N/A": Convert.ToDateTime(item.CreatedDate).ToString("g");
                        EM.UsedDate = item.UsedDate == null?"N/A": Convert.ToDateTime(item.UsedDate).ToString("g");
                        EM.IsUsed = Convert.ToBoolean(item.IsUsed);

                        retData.Add(EM);
                    }
                }
            }
            catch (Exception ex)
            {
                retData = null;
                throw ex;
            }

            return retData;

        }

        public static List<EvoucherModel> GetAllEvoucherDetails(int userID, string Epin)
        {
            List<EvoucherModel> retData = new List<EvoucherModel>();
            try
            {
                var AllEvouchers = db.usp_Get_All_Evoucher_Details(Epin, userID);
                if (AllEvouchers != null)
                {
                    foreach (var item in AllEvouchers.ToList())
                    {
                        EvoucherModel EM = new EvoucherModel();
                        EM.EVouchersID = Convert.ToInt32(item.EVouchersID);
                        EM.CreaterName = item.Username;
                        EM.RecieverName = item.RecievedBy;
                        EM.VoucherCode = item.VoucherCode;
                        EM.CreatedDate = item.CreatedDate == null ? "N/A" : Convert.ToDateTime(item.CreatedDate).ToString("g");
                        EM.UsedDate = item.UsedDate == null ? "N/A" : Convert.ToDateTime(item.UsedDate).ToString("g");
                        EM.IsUsed = Convert.ToBoolean(item.IsUsed);

                        retData.Add(EM);
                    }
                }
            }
            catch (Exception ex)
            {
                retData = null;
                throw ex;
            }

            return retData;

        }
    }
}
