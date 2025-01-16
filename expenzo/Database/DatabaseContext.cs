using System;
using System.IO;
using Microsoft.Data.Sqlite;

public class DatabaseContext
{
    private readonly string _databasePath;

    public DatabaseContext()
{
    
    
        // Define the path for the database in the local application data folder
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var folderPath = Path.Combine(appDataPath, "expenzo");
        Directory.CreateDirectory(folderPath); // Create directory if it doesn't exist
        _databasePath = Path.Combine(folderPath, "expenzo.db");
    
    // catch (Exception ex)
    // {
    //     // Log the exception (you can use any logging framework or simply write to the console)
    //     Console.WriteLine($"An error occurred while creating the database path: {ex.Message}");
    //     throw; 
    // }
}
    // Get a connection to the SQLite database
    public SqliteConnection GetConnection()
    {
        return new SqliteConnection($"Data Source={_databasePath}");
    }
}