using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TenmoClient.Data;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransferDAO
    {
        private IAccountsDAO accountsDAO;

        private readonly string connectionString;
        public TransferSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool UpdateBalance(int userId, decimal updatedBalance)
        {
            User user = new User();
            Accounts account = new Accounts();

            string sql = "update accounts set balance = @updated_balance where user_id = @user_id";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@updated_balance", updatedBalance);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        //public bool SendMoney(int receiverId, int senderId, decimal sendAmount)         //consider implementing this whole arugument as a single object
        //{
        //    Accounts senderObject = accountsDAO.GetAccountBalance(senderId);
        //    Accounts receiverObject = accountsDAO.GetAccountBalance(receiverId);

        //    if (senderObject.Balance > sendAmount)
        //    {
        //        receiverObject.Balance += sendAmount;
        //        senderObject.Balance -= sendAmount;

        //        UpdateBalance(senderId, senderObject.Balance);
        //        UpdateBalance(receiverId, receiverObject.Balance);


        //        LogTransfers(2, 2, senderObject.AccountId, receiverObject.AccountId, sendAmount);
        //        LogTransfers(1, 2, receiverObject.AccountId, senderObject.AccountId, sendAmount);

        //        return true;
        //    }
        //    else
        //    {
        //        return false;     //CHANGE LATER IF NEEDED
        //    }
        //}

        public bool LogTransfers(int transfer_type_id, int transfer_status_id, int accountID_from, int accountID_to, decimal amount)
        {
            string sql = "insert transfers (transfer_type_id, transfer_status_id, account_from, account_to,amount) values(@transfer_type_id, @transfer_status_id, @account_from, @account_to, @amount)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@transfer_type_Id", transfer_type_id);
                    cmd.Parameters.AddWithValue("@transfer_status_Id", transfer_status_id);
                    cmd.Parameters.AddWithValue("@account_from", accountID_from);
                    cmd.Parameters.AddWithValue("@account_to", accountID_to);
                    cmd.Parameters.AddWithValue("@amount", amount);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {
                return false;
            }

        }

        //Separate method for actual transfer record to record in Database

    }
}
