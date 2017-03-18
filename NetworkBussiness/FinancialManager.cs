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
    }
}
