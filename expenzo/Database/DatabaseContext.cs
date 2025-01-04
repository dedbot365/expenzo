using Microsoft.Data.Sqlite;
using System.IO;

namespace expenzo.Database
{
    public class DatabaseContext
    {
        private readonly string _databasePath;

        public DatabaseContext()
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expenzo");
            Directory.CreateDirectory(folderPath);
            _databasePath = Path.Combine(folderPath, "expenzo.db");
        }


        public SqliteConnection GetConnection()
        {
            return new SqliteConnection($"Data Source={_databasePath}");
        }
    }
}