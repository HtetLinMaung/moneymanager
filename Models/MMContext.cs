using Microsoft.EntityFrameworkCore;

namespace moneymanager.Models
{
    public class MMContext : DbContext
    {
        public MMContext(DbContextOptions<MMContext> opt) : base(opt) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}