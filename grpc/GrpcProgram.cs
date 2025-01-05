using grpc;
using grpc.Services;


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

        var app = builder.Build();

        app.MapGrpcService<CounterServer>(); //.EnableGrpcWeb();
        app.MapGet("/", () => "le server gRpc works succefully");

        app.Run();
    }
}


