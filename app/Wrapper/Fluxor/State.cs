using Fluxor;
using grpc;
using Grpc.Core;

namespace app;

[FeatureState]
public record class State
{
    public List<AsyncServerStreamingCall<CounterResponse>> Response { get; set; }
    public State()
    {
        Response = new List<AsyncServerStreamingCall<CounterResponse>>();
    }

}
