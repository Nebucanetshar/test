using Grpc.Core;
using grpc;
using app.Components.Pages;

namespace grpc.Services;

public class GreeterService : Link.LinkBase
{
	private readonly ILogger<GreeterService>_logger;
	public GreeterService(ILogger<GreeterService> logger)
	{
		_logger = logger;
	}

	public override async Task SayHelloStream(CounterRequest request, IServerStreamWriter<CounterResponse> responseStream, ServerCallContext context)
    {
        if (request.Count <=0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "le compteur doit $etre plus grand que Zeros"));
        }
        _logger.LogInformation($"l'utilisateur à appuyer {request.Count} fois");

        for (var i=0; i<request.Count; i++)
        {
            await responseStream.WriteAsync(new CounterResponse { Message = $"tu a réussi a joindre les deux bout {i+1} fois " });
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

}
