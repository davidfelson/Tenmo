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
        private ITransferDAO transferDAO;

        public TransferController(IUserDAO userDAO, TransferServices transferServices, IAccountsDAO accountsDAO, ITransferDAO transferDAO)
        {
            this.userDAO = userDAO;
            this.transferServices = transferServices;     //Don't need a DAO with transferServices in server
            this.accountsDAO = accountsDAO;
            this.transferDAO = transferDAO;
        }

        [HttpGet]
        public List<User> GetListUsers()
        {
            return userDAO.GetUsers();
        }

        [HttpGet("{id}")]
        public List<ViewTransfers> ViewTransfers(int id)
        {
            return transferDAO.ViewTransfers(id);
        }
        [HttpGet("transfer{id}")]
        public Transfers GetTransfer(int id)
        {
            return transferDAO.GetTransfer(id);
        }

        [HttpPost("send")]
        public string SendMoney(Transfers transfers)     
        {
            return transferServices.SendMoney(transfers);     
        }

        [HttpPost("request")]
        public string RequestMoney(Transfers transfers)
        {
            return transferServices.RequestMoney(transfers);
        }
    }
}
