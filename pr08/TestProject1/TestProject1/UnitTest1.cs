using Xunit;
using FixtureDemo;

namespace FixtureDemo.Tests
{
    // 1. Class Fixture с IDisposable
    public class DatabaseFixture : IDisposable
    {
        public DatabaseService Database { get; }
        public int InitializationTime { get; }

        public DatabaseFixture()
        {
            Database = new DatabaseService();
            InitializationTime = Environment.TickCount;
            Console.WriteLine($"DatabaseFixture created at {InitializationTime}");
        }

        public void Dispose()
        {
            Database?.Dispose();
            Console.WriteLine($"DatabaseFixture disposed after {Environment.TickCount - InitializationTime}ms");
        }
    }

    // 2. Class Fixture с IAsyncLifetime
    public class CacheFixture : IAsyncLifetime
    {
        public CacheService Cache { get; private set; }
        public bool IsInitialized { get; private set; }

        public async Task InitializeAsync()
        {
            Cache = new CacheService();
            await Cache.InitializeAsync();
            IsInitialized = true;
            Console.WriteLine("CacheFixture initialized asynchronously");
        }

        public async Task DisposeAsync()
        {
            if (Cache != null)
            {
                await Cache.DisposeAsync();
            }
            Console.WriteLine("CacheFixture disposed asynchronously");
        }
    }

    // 3. Collection Fixture
    

    // 4. Collection Fixture с асинхронной инициализацией
    public class FileSystemFixture : IAsyncLifetime
    {
        public string TestDirectory { get; private set; }
        public int FilesCreated { get; private set; }

        public async Task InitializeAsync()
        {
            TestDirectory = Path.Combine(Path.GetTempPath(), $"Test_{Guid.NewGuid()}");
            Directory.CreateDirectory(TestDirectory);
            FilesCreated = 0;

            // Имитация асинхронной настройки
            await Task.Delay(100);
            Console.WriteLine($"FileSystemFixture initialized with directory: {TestDirectory}");
        }

        public string CreateTestFile(string content = "")
        {
            var filePath = Path.Combine(TestDirectory, $"test_{FilesCreated++}.txt");
            File.WriteAllText(filePath, content);
            return filePath;
        }

        public async Task DisposeAsync()
        {
            if (Directory.Exists(TestDirectory))
            {
                Directory.Delete(TestDirectory, true);
                await Task.Delay(50);
            }
            Console.WriteLine("FileSystemFixture disposed");
        }
    }

    // Тестовый класс с Class Fixture (IDisposable)
    public class DatabaseServiceTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public DatabaseServiceTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            Console.WriteLine($"DatabaseServiceTests constructor - Fixture initialized {Environment.TickCount - _fixture.InitializationTime}ms ago");
        }

        [Fact]
        public async Task GetUserAsync_ValidId_ReturnsUser()
        {
            // Arrange
            var userId = 1;

           
      
        }


        [Fact]
        public void DatabaseConnection_SharedBetweenTests_SameInstance()
        {
            // Assert
            Assert.NotNull(_fixture.Database);
            Assert.Equal(1, _fixture.Database.ConnectionCount);
        }
    }

    // Тестовый класс с Class Fixture (IAsyncLifetime)
    public class CacheServiceTests : IClassFixture<CacheFixture>
    {
        private readonly CacheFixture _fixture;

        public CacheServiceTests(CacheFixture fixture)
        {
            _fixture = fixture;
            Console.WriteLine("CacheServiceTests constructor");
        }


    
    }

    // Определение коллекции для Collection Fixture
   
    // Тестовые классы в одной коллекции разделяют фикстуру
    [Collection("ConfigurationCollection")]
    public class ConfigurationTests1
    {
        

 
        
    }

    [Collection("ConfigurationCollection")]
    public class ConfigurationTests2
    {
       
       
    }

    // Определение коллекции для асинхронной фикстуры
    [CollectionDefinition("FileSystemCollection")]
    public class FileSystemCollection : ICollectionFixture<FileSystemFixture>
    {
    }

    [Collection("FileSystemCollection")]
    public class FileSystemTests1
    {
        private readonly FileSystemFixture _fixture;

        public FileSystemTests1(FileSystemFixture fixture)
        {
            _fixture = fixture;
        }

      
    }

    [Collection("FileSystemCollection")]
    public class FileSystemTests2
    {
        private readonly FileSystemFixture _fixture;

        public FileSystemTests2(FileSystemFixture fixture)
        {
            _fixture = fixture;
        }

        
    }

    // Комплексный пример с несколькими фикстурами
    public class ComplexFixture : IAsyncLifetime
    {
        public DatabaseService Database { get; private set; }
        public CacheService Cache { get; private set; }
        public string TestData { get; private set; }

        public async Task InitializeAsync()
        {
            Database = new DatabaseService();
            Cache = new CacheService();
            await Cache.InitializeAsync();

            TestData = "Initialized at " + DateTime.Now.ToString("HH:mm:ss.fff");
            Console.WriteLine($"ComplexFixture initialized: {TestData}");

            await Task.Delay(100); // Имитация сложной инициализации
        }

        public async Task DisposeAsync()
        {
            Database?.Dispose();
            if (Cache != null)
            {
                await Cache.DisposeAsync();
            }
            Console.WriteLine("ComplexFixture disposed");
        }
    }

    public class IntegrationTests : IClassFixture<ComplexFixture>
    {
        private readonly ComplexFixture _fixture;

        public IntegrationTests(ComplexFixture fixture)
        {
            _fixture = fixture;
        }

      
    }

    // Тесты для демонстрации времени жизни фикстур
    public class FixtureLifetimeTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private static int _testCounter = 0;
        private readonly int _instanceNumber;

        public FixtureLifetimeTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _instanceNumber = ++_testCounter;
            Console.WriteLine($"FixtureLifetimeTests instance {_instanceNumber} created");
        }

        [Fact]
        public void Test1_FixtureIsShared()
        {
            Console.WriteLine($"Test1 running in instance {_instanceNumber}");
            Assert.NotNull(_fixture.Database);
        }

        [Fact]
        public void Test2_FixtureIsSameInstance()
        {
            Console.WriteLine($"Test2 running in instance {_instanceNumber}");
            Assert.NotNull(_fixture.Database);
        }

        
    }
}