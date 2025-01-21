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

        // configuration du service gRpc et transcoding si utilisation du protocol http.1.1
        services.AddGrpc();//.AddJsonTranscoding();


        //configuration DI de la base de donnée avec LinqToDb
        services.AddLinqToDBContext<AppDataConnection>((provider, options) =>

         options.UsePostgreSQL(connectionString));



        #region Programme généré 
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.MapGrpcService<CounterServer>();
        app.MapGet("/", () => "gRpc works succesfully");
        app.Run();
        #endregion
    }
}


