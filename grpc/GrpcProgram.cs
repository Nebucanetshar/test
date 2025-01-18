using grpc;
using grpc.Services;
using Grpc.AspNetCore.Web;
using System.Diagnostics;

using Microsoft.AspNetCore.Server.Kestrel.Core;

public class GrpcProgram
{
    public static void Main (string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // configuration du transcoding
        builder.Services.AddGrpc();//.AddJsonTranscoding();
        
        
        
        // configuration de la base de donnée 
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            builder.Configuration.GetConnectionString("vans");
        });

        
        #region Programme généré 
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.MapGrpcService<CounterServer>();
        app.MapGet("/", () => "gRpc works succesfully");
        app.Run();
        #endregion
    }
}


