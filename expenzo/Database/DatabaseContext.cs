using Microsoft.Data.Sqlite;
using System.IO;

namespace expenzo.Database
{
    public class DatabaseContext
    {
        private readonly string _databasePath;

        public DatabaseContext()
        {
            // This will create an database directory in the solution folder that will be utilized to store the database file
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "database"); 
            Directory.CreateDirectory(folderPath);
            _databasePath = Path.Combine(folderPath, "expenzo.db");
        }

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection($"Data Source={_databasePath}");
        }
    }
}