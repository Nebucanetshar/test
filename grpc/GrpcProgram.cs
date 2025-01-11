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

        // ajout du transcoding json-grpc avec annotation google 
        builder.Services.AddGrpc(); //.AddJsonTranscoding();

        //configuration ReflectionService pour visualisé gRpcui
        builder.Services.AddGrpcReflection();
        
        // configuration base de donnée 
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            builder.Configuration.GetConnectionString("vans");
        });

        var app = builder.Build();

        app.UseGrpcWeb();
        app.UseRouting();

        app.MapGrpcService<CounterServer>(); //.EnableGrpcWeb();
        
        
        if (app.Environment.IsDevelopment())
        
            app.MapGrpcReflectionService();
        
        
        app.MapGet("/", () => "le server gRpc works succefully");

        app.Run();
    }
}


