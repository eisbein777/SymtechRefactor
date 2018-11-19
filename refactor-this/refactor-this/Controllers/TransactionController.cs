using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using refactor_this.Business.Dtos;
using refactor_this.Business.Service.Transactions;
using refactor_this.Data.Service;

namespace refactor_this.Controllers
{
    public class TransactionController : ApiController
    {
        [HttpGet, Route("api/Accounts/{id}/Transactions")]
        public IHttpActionResult GetTransactions(Guid id)
        {
            if (ModelState.IsValid)
            {

                TransactionBusiness transactionsmod = new TransactionBusiness();
                var transactions = transactionsmod.GetTransactions(id);

                return Ok(transactions);
            }
            else
                return BadRequest("Could not insert the transaction");

        }

        [HttpPost, Route("api/Accounts/{id}/Transactions")]
        public IHttpActionResult AddTransaction(Guid id, Transaction transaction)
        {
            TransactionBusiness transactionsmod = new TransactionBusiness();

            var result =  transactionsmod.CreateTransaction(id, transaction);

            if (result == true)
                return Ok();
            else
                return BadRequest("Could not Add the transaction");
     
        }

        [HttpPost, Route("api/Accounts/{id}/Transactions")]
        public IHttpActionResult UpdateTransaction(Guid id, Transaction transaction)
        {
            TransactionBusiness transactionsmod = new TransactionBusiness();

            var result = transactionsmod.UpdateTransaction(id, transaction);

            if (result == true)
                return Ok();
            else
                return BadRequest("Could not Update the transaction");

        }
    }
}