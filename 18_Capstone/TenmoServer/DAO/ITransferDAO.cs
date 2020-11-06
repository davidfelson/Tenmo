namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        bool UpdateBalance(int userId, decimal updatedBalance);

        bool SendMoney(int receiverId, int senderId, decimal sendAmount);

        bool LogTransfers(int transfer_type_id, int transfer_status_id, int accountID_from, int accountID_to, decimal amount);
    }
}