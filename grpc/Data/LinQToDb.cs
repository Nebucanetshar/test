using LinqToDB.Data;
using LinqToDB;

namespace grpc;

public class AppDataConnection : DataConnection
{
    public ITable<ToDb> ToDb => this.GetTable<ToDb>();
    public AppDataConnection(DataOptions<AppDataConnection> options) : base(ProviderName.PostgreSQL, "Host=LocalHost; Database=linQ; UserName=postgres; Password=A1996b4860150*") { }
}
