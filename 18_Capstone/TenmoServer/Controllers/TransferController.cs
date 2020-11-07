using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoClient.Data;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Services;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private IUserDAO userDAO;
        private TransferServices transferServices;
        private IAccountsDAO accountsDAO;

        public TransferController(IUserDAO userDAO, TransferServices transferServices, IAccountsDAO accountsDAO)
        {
            this.userDAO = userDAO;
            this.transferServices = transferServices;     //Don't need a DAO with transferServices in server
            this.accountsDAO = accountsDAO;
        }

        [HttpGet]
        public List<User> GetListUsers()
        {
            return userDAO.GetUsers();
        }

        [HttpPost]
        public string SendMoney(Transfers transfers)     
        {
            return transferServices.SendMoney(transfers);     
        }

        [HttpPost]
        public string RequestMoney(Transfers transfers)
        {
            return transferServices.RequestMoney(transfers);
        }
    }
}
