using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace grpc;

public class ConnectionStringSettings : IConnectionStringSettings
{
    public string ConnectionString { get; set; }
    public string Name { get; set; }
    public string? ProviderName { get; set; }
    public bool IsGlobal => false;
}

public class Setting : ILinqToDBSettings
{
    public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();
    public string DefaultConfiguration => "PostgreSQL";
    public string DefaultDataProvider => "PostgreSQL";


    public Setting() { }
    
    public IEnumerable<IConnectionStringSettings> ConnectionStrings => new[]
    {
        new ConnectionStringSettings
        {
            Name = "PostgreSQL",
            ProviderName = "PostgreSQL",
            ConnectionString = "Host=LocalHost; Database=linQ; UserName=postgres; Password=A1996b4860150*"
        }
    };
}

public class LinkToDb
{
    public DataOptions _dataOptions;
    public DataOptions<AppDataConnection> _options;
    public AppDataConnection _connection;

    public LinkToDb()
    {
        _dataOptions = new DataOptions();
        _options = new DataOptions<AppDataConnection>(_dataOptions);
        _connection = new AppDataConnection(_options);
    }

    public void CreateTable()
    {
        DataConnection.DefaultSettings = new Setting();
        _connection.CreateTable<ToDb>();
    }
}
