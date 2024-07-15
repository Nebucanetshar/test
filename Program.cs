using grpc.Data;
using gRpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc(); //.AddJsonTranscoding();
builder.Services.AddDbContext<AppDbContext>(options=>options.UseNpgsql(builder.Configuration.GetConnectionString("backTest")));

var app = builder.Build();


app.MapGrpcService<GreeterService>();


app.Run();
