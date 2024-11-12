using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvestmentManagementAPI.Models;
using InvestmentManagementAPI.DTO;

namespace InvestmentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly InvestmentDBContext _context;
        private readonly ILogger<TransactionsController> _logger;
        public TransactionsController(InvestmentDBContext context, ILogger<TransactionsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction()
        {
            _logger.LogInformation("Recieved a Transaction Get request");
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            _logger.LogInformation($"Recieved a Transaction Get request by id: {id}");
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, TransactionDTO transactionDTO)
        {
            Transaction transaction=new Transaction();
            transaction.TransactionId=transactionDTO.TransactionId;
            transaction.AssetId=transactionDTO.AssetId;
            transaction.TransactionType=transactionDTO.TransactionType;
            transaction.Amount=transactionDTO.Amount;
            transaction.TransactionDate=transactionDTO.TransactionDate;

            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated a Transaction Put request by id: {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionDTO transactionDTO)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionId = transactionDTO.TransactionId;
            transaction.AssetId = transactionDTO.AssetId;
            transaction.TransactionType = transactionDTO.TransactionType;
            transaction.Amount = transactionDTO.Amount;
            transaction.TransactionDate = transactionDTO.TransactionDate;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Updated a Transaction Post request by id: {transaction.TransactionId}");

            return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Deleted a Transaction data belongs to id: {id}");

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
