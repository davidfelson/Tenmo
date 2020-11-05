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

        public TransferController(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        [HttpGet]
        public List<User> GetListUsers()
        {
            return userDAO.GetUsers();
            //UserSqlDAO userSqlDAO = new UserSqlDAO();
            //return userSqlDAO.GetUsers();
        }
    }
}
