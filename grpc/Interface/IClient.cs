using Grpc.Core;

namespace grpc;

public interface IClient
{
    AsyncServerStreamingCall<CounterResponse> StartCounter(CounterRequest request);
}
