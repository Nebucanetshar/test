using grpc.Services;
using grpc.Data;
using Microsoft.EntityFrameworkCore;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("pgsl")));

        builder.Services.AddGrpc();

        var app = builder.Build();


        app.MapGrpcService<GreeterService>();
        app.MapGrpcService<Server>();

        app.Run();
    }
}