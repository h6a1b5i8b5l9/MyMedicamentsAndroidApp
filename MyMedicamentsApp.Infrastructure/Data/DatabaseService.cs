using SQLite;
using System.IO;

namespace MyMedicamentsApp.Infrastructure.Data
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        
        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyMedicamentsApp.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            InitializeDatabaseAsync().Wait();
        }
        
        public SQLiteAsyncConnection Database => _database;
        
        private async Task InitializeDatabaseAsync()
        {
            await _database.CreateTableAsync<MedicamentEntity>();
        }
    }
} 