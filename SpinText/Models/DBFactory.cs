using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TL.Methods;

namespace SpinText.Models
{
    public class DBFactory
    {
        static Db _db;
        string _connectionString;
        public DBFactory(IConfiguration conf) 
        {
            _connectionString = conf.GetConnectionString("DefaultConnection") ?? "";
        }

        public Db Create()
        {
            //if (_db != null) return _db;

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            
            var options = new DbContextOptionsBuilder()
                .UseMySql(_connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            _db = new Db(options);
            return _db;
        }

        public void Remove()
        {
            if (_db is null) return;
            //_db.Update(_db.Templates);
        }
    }
}
