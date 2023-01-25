using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SpinText.Blocks.DB;
using SpinText.HT.DB;

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

        public Db(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
