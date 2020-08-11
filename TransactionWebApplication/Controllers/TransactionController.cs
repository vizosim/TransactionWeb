using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionWebApplication.Core;
using TransactionWebApplication.Entities;

namespace TransactionWebApplication.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionLogStore _transactionLogStore;
        private readonly TransactionManager _transactionManager;

        public TransactionController(TransactionLogStore transactionLogStore, TransactionManager transactionManager)
        {
            _transactionLogStore = transactionLogStore;
            _transactionManager = transactionManager;
        }

        [HttpGet("balance")]
        public async Task<ActionResult<decimal>> GetBalance()
        {
            var balance = await _transactionManager.ReadAmount();
            return Ok(new { Balance = balance });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get()
        {
            IEnumerable<Transaction> transactions = await _transactionLogStore.GeTransactionLog();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public ActionResult<Transaction> Get(string id)
        {
            var transaction = _transactionLogStore.GeTransaction(id);
            if (transaction == null)
                return NotFound();
            return Ok(transaction);
        }

        [HttpPost("commit")]
        public async Task<ActionResult<Transaction>> CommitTransaction([FromBody] Transaction transaction)
        {
            OperationResult result = await _transactionManager.CommitTransaction(transaction);

            if (result.Status == TransactionStatus.Rejected)
                return Conflict($"Transaction rejected due to: {result.Message}");

            if (result.Status == TransactionStatus.Failed)
                return BadRequest($"Transaction failed due to: {result.Message}");

            return Ok(new { Id = result.TransactionId, Status = result.Status });
        }
    }
}
