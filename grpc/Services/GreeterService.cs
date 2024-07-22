using Grpc.Core;
using grpc;
using Microsoft.AspNetCore.WebUtilities;

namespace grpc.Services;
//public interface IGrpcUserServiceClientWrapper
//{
//    Task<ResponseWrapperViewModel<IEnumerable<UserViewModel>>> GetOperatorsAsync();

    
//}
public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

   public override async Task SayHelloStream(HelloRequestCount request,IServerStreamWriter<HelloReply> responseSteam, ServerCallContext context)
    {
        if (request.Count <= 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Le count doit être plus grand que zéros"));

        _logger.LogInformation($"envoie{request.Count} salut pour {request.Name}");

        for (var i=0; i<request.Count; i++)
        {
            await responseSteam.WriteAsync(new HelloReply { Message=$"Salut {request.Name} {i+1}" });
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
