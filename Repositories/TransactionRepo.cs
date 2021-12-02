using Microsoft.EntityFrameworkCore;
using moneymanager.Models;

namespace moneymanager.Repositories
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly MMContext _context;

        public TransactionRepo(MMContext context)
        {
            _context = context;
        }
        public async Task<bool> AddTransaction(Transaction transaction)
        {
            transaction.CreatedAt = DateTime.Now;
            transaction.UpdatedAt = DateTime.Now;
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTransaction(long id)
        {
            var old = await GetTransactionById(id);
            if (old == null)
            {
                return false;
            }
            _context.Transactions.Remove(old);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Transaction?> GetTransactionById(long id) => await _context.Transactions.FindAsync(id);

        public IEnumerable<Transaction> GetTransactions(int page, int perpage, string search)
        {
            if (page != 0 && perpage != 0 && !String.IsNullOrEmpty(search))
            {
                var skip = (page - 1) * perpage;
                return _context.Transactions.Where(t => t.ItemName.Equals(search)).Skip(skip).Take(perpage).OrderBy(t => t.CreatedAt).ToList();
            }
            else if (page != 0 && perpage != 0 && String.IsNullOrEmpty(search))
            {
                var skip = (page - 1) * perpage;
                return _context.Transactions.Skip(skip).Take(perpage).OrderBy(t => t.CreatedAt).ToList();
            }
            else
            {
                return _context.Transactions.ToList();
            }
        }

        public int GetTransactionsCount(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                return _context.Transactions.Where(t => t.ItemName.Equals(search)).OrderBy(t => t.CreatedAt).Count();
            }
            else if (String.IsNullOrEmpty(search))
            {
                return _context.Transactions.OrderBy(t => t.CreatedAt).Count();
            }
            else
            {
                return _context.Transactions.Count();
            }
        }

        public async Task<bool> UpdateTransaction(long id, Transaction transaction)
        {
            var old = await GetTransactionById(id);
            if (old == null)
            {
                return false;
            }
            old.ItemName = transaction.ItemName;
            old.Qty = transaction.Qty;
            old.Price = transaction.Price;
            old.UpdatedAt = DateTime.Now;
            _context.Entry(old).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}