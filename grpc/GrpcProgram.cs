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

        // Add services to the container.
        builder.Services.AddGrpc().AddJsonTranscoding();
        
        // configuration base de donnée 
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            builder.Configuration.GetConnectionString("vans");
        });
        //configuration du TLS 
        //builder.WebHost.ConfigureKestrel(options =>
        //{
        //    options.ListenLocalhost(5269, listenOptions =>
        //    {
        //        try
        //        {
        //            listenOptions.UseHttps("certificat.pfx", "A1996b4860150*");
        //        }
        //        catch(Exception ex) 
        //        {
        //            Trace.TraceInformation($"erreur lors du chargement du certicat : {ex.Message}");
        //        }
                
        //        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
        //    });
        //});

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<GreeterService>();
        app.MapGrpcService<CounterServer>().EnableGrpcWeb();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


        app.Run();
    }
}


