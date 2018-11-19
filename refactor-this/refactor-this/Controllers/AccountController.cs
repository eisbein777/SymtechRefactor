using refactor_this;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using refactor_this.Business.Dtos;
using refactor_this.Data.Service;

using refactor_this.Business.Service.Accounts;

namespace refactor_this.Controllers
{
    

    public class AccountController : ApiController
    {


        [HttpGet, Route("api/Accounts/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                AccountBusiness accountingmod = new AccountBusiness();


                return Ok(accountingmod.Get(id));
            }
            else
            {
                return BadRequest("ID not Valid");
            }
        }

        [HttpGet, Route("api/Accounts")]
        public IHttpActionResult Get()
        {


                AccountBusiness Accountingmod = new AccountBusiness();

                var accounts = Accountingmod.GetAllAccounts();
                return Ok(accounts);
     
        }

   

        [HttpPost, Route("api/Accounts")]
        public IHttpActionResult Add(Account account)
        {
            if (ModelState.IsValid)
            {
                AccountBusiness accountingmod = new AccountBusiness();
                accountingmod.Save(account);
                return Ok();
            }

            return BadRequest("Account not Valid");



        }

        [HttpPut, Route("api/Accounts/{id}")]
        public IHttpActionResult Update(Guid id, Account account)
        {
            if (ModelState.IsValid)
            {
                AccountBusiness accountingmod = new AccountBusiness();
                var existing = accountingmod.Get(id);
                existing.Name = account.Name;
                accountingmod.Save(existing);
                return Ok();
            }

            return BadRequest("Account not Valid");
        }

        [HttpDelete, Route("api/Accounts/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                AccountBusiness accountingmod = new AccountBusiness();
            var existing = accountingmod.Get(id);
            accountingmod.Delete(existing);
            return Ok();
            }

            return BadRequest("ID Not Valid");
        }
    }
}