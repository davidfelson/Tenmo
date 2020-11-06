using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public TransferController(IUserDAO userDAO, ITransferDAO transferDAO)
        {
            this.userDAO = userDAO;
            this.transferDAO = transferDAO;
        }

        [HttpGet]
        public List<User> GetListUsers()
        {
            return userDAO.GetUsers();
            //UserSqlDAO userSqlDAO = new UserSqlDAO();
            //return userSqlDAO.GetUsers();
        }

        [HttpPost]
        public bool SendMoney(int receiverId, int senderId, decimal sendAmount)
        {
            return transferDAO.SendMoney(receiverId, senderId, sendAmount);
        }
    }
}
