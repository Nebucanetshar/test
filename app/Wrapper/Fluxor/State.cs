using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;

[FeatureState(CreateInitialStateMethodName = nameof(LazyInitializer))]
public class State
{ 
    public List<AsyncServerStreamingCall<CounterResponse>>? _Flux { get; set; }

    //public State(List<AsyncServerStreamingCall<CounterResponse>> Flux)
    //{
    //    _Flux = Flux;
    //}
    //public State(IEnumerable<AsyncServerStreamingCall<CounterResponse>> updateMessages)
    //{
    //    _Flux = new List<AsyncServerStreamingCall<CounterResponse>>();
    //}
    public State() { }
    private static State LazyInitializer()
    {
        return new State
        {
            _Flux = new List<AsyncServerStreamingCall<CounterResponse>>()
        };
    }
}
