using grpc;
using grpc.Services;
using LinqToDB.AspNet;
using LinqToDB;


public class GrpcProgram
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = builder.Configuration;
        var connectionString = configuration.GetConnectionString("linQ") ?? throw new InvalidOperationException("arg");

        ///<summary>
        /// configuration du service gRpc coté server
        ///</summary>
        services.AddGrpc();

        ///<summary>
        ///configuration DI de LinqToDb
        ///</summary>
        services.AddLinqToDBContext<AppDataConnection>((provider, options) =>
         options.UsePostgreSQL(connectionString));

        ///<summary>
        ///configuration de la migration vers la base de donnée PostgreSQL
        ///</summary>
        //LinkToDb migration = new LinkToDb();
        //migration.CreateTable();

        #region Programme généré 
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.MapGrpcService<CounterServer>();
        app.MapGet("/", () => "gRpc works succesfully");
        app.Run();
        #endregion
    }
}


