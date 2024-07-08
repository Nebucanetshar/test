using Grpc.Core;
using gRpc;
using Microsoft.Extensions.Logging;

namespace gRpc.Services;

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

    public override async Task SayHelloStream(HelloRequestCount request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
    {
        if (request.Count <= 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Count must be greater than zero"));
        
        _logger.LogInformation($"send {request.Count} hellos to {request.Name}");

        for (var i =0; i < request.Count; i++)
        {
            
            await responseStream.WriteAsync(new HelloReply { Message=$"Hello{request.Name} {i+1}"});
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
        
        
       

}
