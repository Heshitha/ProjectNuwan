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
                    AccountNumber = BankDetails.AccountNumber,
                    Amount = (float)BankDetails.Amount,
                    BankName = BankDetails.BankName,
                    Nic = BankDetails.Nic,
                    Address = BankDetails.Address,
                    IsComplete = false
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

        public static List<BankTransferModel> GetAllBankDetails()
        {
            List<BankTransferModel> lBtm = new List<BankTransferModel>();
            try
            {
                var AllBanktransfers = db.usp_Get_All_Bank_Details();

                if (AllBanktransfers != null)
                {
                    foreach (var item in AllBanktransfers)
                    {
                        BankTransferModel btm = new BankTransferModel();
                        btm.ID = item.ID;
                        btm.Username = item.Username;
                        btm.TransferType = item.TransferType;
                        btm.AccType = item.AccType;
                        btm.AccountName = item.AccountName;
                        btm.AccountNumber = item.AccountNumber;
                        btm.Amount = (float)item.Amount;
                        btm.BankName = item.BankName;
                        btm.Nic = item.Nic;
                        btm.Address = item.Address;

                        lBtm.Add(btm);
                    }

                }
            }
            catch (Exception ex )
            {
                lBtm = null;
                throw ex;
            }

            return lBtm;

        }

        public static BankTransferModel GetBankDetailById(int ID)
        {
            BankTransferModel btm = new BankTransferModel();
            try
            {
                var result = db.usp_GetBankDetailsById(ID).SingleOrDefault();

                if (result != null)
                {

                        btm = new BankTransferModel();

                        btm.Username = result.Username;
                        btm.TransferType = result.TransferType;
                        btm.AccType = result.AccType;
                        btm.AccountName = result.AccountName;
                        btm.AccountNumber = result.AccountNumber;
                        btm.Amount = (float)result.Amount;
                        btm.BankName = result.BankName;
                        btm.Nic = result.Nic;
                        btm.Address = result.Address;
                        btm.ProofUrl = result.ProofUrl;
                }
            }
            catch (Exception ex)
            {
                btm = null;
                throw ex;
            }

            return btm;

        }

        public static List<BankTransferModel> GetAllBankDetails(string Username)
        {
            List<BankTransferModel> lBtm = new List<BankTransferModel>();
            try
            {
                var AllBanktransfers = db.usp_GetBankDetailsByUsername(Username);

                if (AllBanktransfers != null)
                {
                    foreach (var item in AllBanktransfers)
                    {
                        BankTransferModel btm = new BankTransferModel();

                        btm.Username = item.Username;
                        btm.TransferType = item.TransferType;
                        btm.AccType = item.AccType;
                        btm.AccountName = item.AccountName;
                        btm.AccountNumber = item.AccountNumber;
                        btm.Amount = (float)item.Amount;
                        btm.BankName = item.BankName;
                        btm.Nic = item.Nic;
                        btm.Address = item.Address;

                        lBtm.Add(btm);
                    }

                }
            }
            catch (Exception ex)
            {
                lBtm = null;
                throw ex;
            }

            return lBtm;

        }

        public static int CreateEpins(List<EpinModel> Epins)
        {
            int retval = 0;
            try
            {
                foreach (var item in Epins)
                {
                    var epin = db.EVouchers.Where(x => x.VoucherCode == item.VoucherCode).SingleOrDefault();
                    if(epin==null)
                    {
                        DateTime utcTime = DateTime.UtcNow;
                        var tz = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                        var tzTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tz);

                        EVoucher Ev = new EVoucher()
                        {
                            CreaterID = item.CreaterID,
                            VoucherCode = item.VoucherCode,
                            CreatedDate = tzTime,
                            EpinValue = item.Epinvalue,
                            IsUsed = false
                        };
                        db.EVouchers.InsertOnSubmit(Ev);
                        db.SubmitChanges();
                    }
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

        public static int uploadProof(string url, int ID)
        {
            var bankdetails = db.BankDetails.Where(x => x.ID == ID).FirstOrDefault();

            if(bankdetails!=null)
            {
                bankdetails.ProofUrl = url;
                db.SubmitChanges();
            }


            return 1;
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
