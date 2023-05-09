using HistoryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HistoryAPI.Repository
{
    public class HistoryDbContext : DbContext
    {
        public HistoryDbContext(DbContextOptions<HistoryDbContext> options) : base(options) { }

        public DbSet<History> SearchHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>().HasKey(e => e.Id);
        }
    }
}
