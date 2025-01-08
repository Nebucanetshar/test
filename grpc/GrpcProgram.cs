using grpc;
using grpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;


public class GrpcProgram
{
    public static void Main (string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // added for trancoding json-grpc 
        builder.Services.AddGrpc(); //.AddJsonTranscoding();
        builder.Services.AddGrpcReflection();
        
        // configuration base de donnée 
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            builder.Configuration.GetConnectionString("vans");
        });

        //configuration de délais de requête 
        //builder.Services.AddGrpc(options =>
        //{
        //    options.MaxReceiveMessageSize = 1024;
        //    options.MaxSendMessageSize = 1024;
        //});

        //configuration du kestrel pour activé le protocols Http2
        //builder.WebHost.ConfigureKestrel(options =>
        //{
        //    options.ListenLocalhost(7226, listenOptions =>
        //    {
        //        listenOptions.Protocols = HttpProtocols.Http2;
        //        listenOptions.UseHttps();
        //    });
        //});

        //builder.Logging.ClearProviders();
        //builder.Logging.AddConsole();
        //builder.Logging.SetMinimumLevel(LogLevel.Debug);

        var app = builder.Build();

        app.UseGrpcWeb();
        app.UseRouting();

        app.MapGrpcService<CounterServer>(); //.EnableGrpcWeb();
        if (app.Environment.IsDevelopment())
        {
            app.MapGrpcReflectionService();
        }
        
        app.MapGet("/", () => "le server gRpc works succefully");

        app.Run();
    }
}


