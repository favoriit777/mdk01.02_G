namespace FixtureDemo
{
    public class DatabaseService : IDisposable
    {
        private bool _disposed = false;
        public List<string> ExecutedQueries { get; } = new();
        public int ConnectionCount { get; private set; }

        public DatabaseService()
        {
            // Имитация установки соединения с БД
            ConnectionCount++;
            Console.WriteLine("Database connection opened");
        }

        


        public void Dispose()
        {
            if (!_disposed)
            {
                ConnectionCount--;
                Console.WriteLine("Database connection closed");
                _disposed = true;
            }
        }
    }

    public class FileProcessor : IAsyncDisposable
    {
        private string _tempFilePath;
        public int ProcessedFiles { get; private set; }

        public FileProcessor()
        {
            _tempFilePath = Path.GetTempFileName();
            Console.WriteLine($"Temp file created: {_tempFilePath}");
        }

        public async Task WriteToFileAsync(string content)
        {
            await File.WriteAllTextAsync(_tempFilePath, content);
            ProcessedFiles++;
        }

        public async Task<string> ReadFromFileAsync()
        {
            return await File.ReadAllTextAsync(_tempFilePath);
        }

        public async ValueTask DisposeAsync()
        {
            if (_tempFilePath != null && File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
                Console.WriteLine($"Temp file deleted: {_tempFilePath}");
                _tempFilePath = null;
            }
            await Task.CompletedTask;
        }
    }

    public class CacheService : IAsyncLifetime
    {
        private readonly Dictionary<string, object> _cache = new();

        public async Task InitializeAsync()
        {
            // Имитация инициализации кэша
            await Task.Delay(100);
            Console.WriteLine("Cache service initialized");
        }



        public async Task DisposeAsync()
        {
            _cache.Clear();
            await Task.Delay(50);
            Console.WriteLine("Cache service disposed");
        }
    }

    public class User
    {
        private string v1;
        private string v2;

       
        public int Id { get; set; }


        public class Configuration
        {
            public string Environment { get; set; } = "Test";
            public string DatabaseConnection { get; set; } = "Server=test;Database=testdb";
            public int MaxConnections { get; set; } = 10;
        }
    }
}