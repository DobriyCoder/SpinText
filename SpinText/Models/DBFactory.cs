using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SpinText.Models
{
    public class DBFactory
    {
        string _connectionString;
        public DBFactory(IConfiguration conf) 
        {
            _connectionString = conf.GetConnectionString("DefaultConnection") ?? "";
        }

        public Db Create()
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            
            var options = new DbContextOptionsBuilder()
                .UseMySql(_connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;

            return new Db(options);
        }
    }
}
