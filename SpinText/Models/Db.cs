using Microsoft.EntityFrameworkCore;
using SpinText.Blocks.DB;
using SpinText.HTProvider.DB;

namespace SpinText.Models
{
    public class Db : DbContext
    {
        public DbSet<BlockData> Blocks { get; set; }
        public DbSet<HTData> Templates { get; set; }
        public Db(DbContextOptions<Db> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
