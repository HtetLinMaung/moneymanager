using moneymanager.Models;

namespace moneymanager.Repositories
{
    public interface ITransactionRepo
    {
        Task<bool> AddTransaction(Transaction transaction);

        IEnumerable<Transaction> GetTransactions(int page, int perpage, string search);

        int GetTransactionsCount(string search);

        Task<Transaction?> GetTransactionById(long id);

        Task<bool> UpdateTransaction(long id, Transaction transaction);

        Task<bool> DeleteTransaction(long id);
    }
}