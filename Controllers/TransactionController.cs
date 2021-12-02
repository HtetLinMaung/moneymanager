using Microsoft.AspNetCore.Mvc;
using moneymanager.Models;
using moneymanager.Repositories;

namespace moneymanager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepo _transrepo;

        public TransactionsController(ITransactionRepo transrepo)
        {
            _transrepo = transrepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> GetTransactions(int page, int perpage, string? search)
        {
            try
            {
                var transactions = _transrepo.GetTransactions(page, perpage, search);
                var total = _transrepo.GetTransactionsCount(search);
                return Ok(new
                {
                    total = total,
                    data = transactions,
                    pagecount = Math.Ceiling((double)total / (double)perpage),
                    page = page,
                    perpage = perpage
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(long id)
        {
            try
            {
                var transaction = await _transrepo.GetTransactionById(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                return Ok(transaction);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> AddTransaction(Transaction transaction)
        {
            try
            {
                var success = await _transrepo.AddTransaction(transaction);
                return CreatedAtAction(nameof(AddTransaction), new { Id = transaction.Id }, transaction);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(long id, Transaction transaction)
        {
            try
            {
                var success = await _transrepo.UpdateTransaction(id, transaction);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(long id)
        {
            try
            {
                var success = await _transrepo.DeleteTransaction(id);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}