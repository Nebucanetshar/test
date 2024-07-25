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

    public override async Task SayHelloStream(CounterRequest request, IServerStreamWriter<CounterResponse> responseStream, ServerCallContext context) 
    {
        if (request.Count <= 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Le compteur doit être plus grand que zéros"));

        _logger.LogInformation($"l'utilisateur à appuyer {request.Count} fois ");

        for (var i = 0; i < request.Count; i++)
        {
            await responseStream.WriteAsync(new CounterResponse { Message = $"tu a réussi à joindre les deux bout {i + 1}" });
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
