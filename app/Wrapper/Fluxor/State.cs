using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;

[FeatureState]
public record class State 
{
    public List<AsyncServerStreamingCall<CounterResponse>> Response { get; set; }
    public State()
    {
        Response = new List<AsyncServerStreamingCall<CounterResponse>>();
    }

}
