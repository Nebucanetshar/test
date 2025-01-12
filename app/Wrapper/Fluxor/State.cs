using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;

[FeatureState]
public record class State
{ 
    public List<AsyncServerStreamingCall<CounterResponse>> Flux { get; set; }
    public State()
    {
        Flux = new List<AsyncServerStreamingCall<CounterResponse>>();
    }

}
