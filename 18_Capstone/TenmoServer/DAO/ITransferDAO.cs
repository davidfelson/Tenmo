using System.Collections.Generic;
using TenmoClient.Data;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        bool UpdateBalance(int userId, decimal updatedBalance);

        //bool SendMoney(int receiverId, int senderId, decimal sendAmount);

        bool LogTransfers(TransferType transfer_type_id, TransferStatus transfer_status_id, int accountID_from, int accountID_to, decimal amount);

        List<ViewTransfers> ViewTransfers(int id);

        Transfers GetTransfer(int id);
    }
}