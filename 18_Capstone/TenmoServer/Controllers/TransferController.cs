using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoClient.Data;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private IUserDAO userDAO;
        private ITransferDAO transferDAO;
        private IAccountsDAO accountsDAO;

        public TransferController(IUserDAO userDAO, ITransferDAO transferDAO, IAccountsDAO accountsDAO)
        {
            this.userDAO = userDAO;
            this.transferDAO = transferDAO;
            this.accountsDAO = accountsDAO;
        }

        [HttpGet]
        public List<User> GetListUsers()
        {
            return userDAO.GetUsers();
            //UserSqlDAO userSqlDAO = new UserSqlDAO();
            //return userSqlDAO.GetUsers();
        }

        [HttpPost]
        public bool SendMoney(Transfers transfers)     //int receiverId, int senderId, decimal sendAmount
        {
            //return transferDAO.SendMoney(receiverId, senderId, sendAmount);
            Accounts senderObject = accountsDAO.GetAccountBalance(transfers.account_from);
            Accounts receiverObject = accountsDAO.GetAccountBalance(transfers.account_to);

            if (senderObject.Balance > transfers.Amount)
            {
                receiverObject.Balance += transfers.Amount;
                senderObject.Balance -= transfers.Amount;

                transferDAO.UpdateBalance(transfers.account_from, senderObject.Balance);
                transferDAO.UpdateBalance(transfers.account_to, receiverObject.Balance);


                transferDAO.LogTransfers(2, 2, senderObject.AccountId, receiverObject.AccountId, transfers.Amount);
                transferDAO.LogTransfers(1, 2, receiverObject.AccountId, senderObject.AccountId, transfers.Amount);

                return true;
            }
            else
            {
                return false;     //CHANGE LATER IF NEEDED
            }
        }
    }
}
