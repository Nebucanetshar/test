using grpc;
using grpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;


public class GrpcProgram
{
    public static void Main (string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // added for trancoding json-grpc 
        builder.Services.AddGrpc(); //.AddJsonTranscoding();
        
        // configuration base de donnée 
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            builder.Configuration.GetConnectionString("vans");
        });

        // configuration du kestrel pour activé le protocols Http2
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenLocalhost(7226, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
                listenOptions.UseHttps();
            });
        });

        var app = builder.Build();

        app.MapGrpcService<CounterServer>(); //.EnableGrpcWeb();
        app.MapGet("/", () => "le server gRpc works succefully");

        app.Run();
    }
}


