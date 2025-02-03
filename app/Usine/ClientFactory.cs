using grpc;
using Grpc.Net.Client;

namespace app;

public class ClientFactory
{
    private readonly GrpcChannel _grpcChannel;

    public ClientFactory(GrpcChannel grpcChannel)
    {
        _grpcChannel = grpcChannel;
    }

    public Counter.CounterClient CreateClient()
    {
        return new Counter.CounterClient(_grpcChannel);
    }
}
