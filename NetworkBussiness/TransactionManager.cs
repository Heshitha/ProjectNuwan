using NetworkDataAccess;
using NetworkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkBussiness
{
    public class TransactionManager
    {
        public static List<TransactionModel> GetAllTransactionsByUser(int userID)
        {
            return TransactionDataAccess.GetAllTransactionsByUser(userID);
        }

        public static bool CheckTransactionKey(int userID, string TransctionKey)
        {
            return TransactionDataAccess.CheckTransactionKey(userID, TransctionKey);
        }

    }
}
