using Microsoft.EntityFrameworkCore;
using ConsolidationService.Models;

namespace ConsolidationService.Data
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }
        public DbSet<Transaction> Transactions { get; set; }
    }
}