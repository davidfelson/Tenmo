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



        //NEED TO CAST TO TransferType/Status when we read the transfers in



        public bool LogTransfers(TransferType transfer_type_id, TransferStatus transfer_status_id, int accountID_from, int accountID_to, decimal amount)
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
        public List<ViewTransfers> ViewTransfers(int user_id)
        {
            List<ViewTransfers> listTransfers = new List<ViewTransfers>();
            string sql = "select t.transfer_id, t.transfer_type_id, u.username, t.amount from users u Join accounts a ON u.user_id = a.user_id Join transfers t ON t.account_from = a.account_id where t.account_from != @user_id OR t.account_to != @user_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ViewTransfers viewTransfers = new ViewTransfers();
                            viewTransfers.transfer_id = Convert.ToInt32(rdr["transfer_id"]);
                            viewTransfers.transfer_type_id = (TransferType)Convert.ToInt32(rdr["transfer_type_id"]);
                            viewTransfers.Username = Convert.ToString(rdr["username"]);
                            viewTransfers.Amount = Convert.ToDecimal(rdr["amount"]);

                            listTransfers.Add(viewTransfers);
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return listTransfers;
        }


        public Transfers GetTransfer(int transfer_id)
        {
            Transfers transfer = new Transfers();
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select *from transfers where transfer_id = @id ", conn);

                    cmd.Parameters.AddWithValue("@id", transfer_id);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {

                            transfer.transfer_id = Convert.ToInt32(rdr["transfer_id"]);
                            transfer.transfer_type_id = (TransferType)Convert.ToInt32(rdr["transfer_type_id"]);
                            transfer.transfer_status_id = (TransferStatus)Convert.ToInt32(rdr["transfer_status_id"]);
                            transfer.Amount = Convert.ToDecimal(rdr["amount"]);
                            transfer.account_from = Convert.ToInt32(rdr["account_from"]);
                            transfer.account_to = Convert.ToInt32(rdr["account_to"]);

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return transfer;

        }
    }

}


