using Grpc.Core;
using grpc;

namespace app;

public class GrpcFlux
{
    public AsyncServerStreamingCall<CounterResponse>? Stream { get; set; }
}
