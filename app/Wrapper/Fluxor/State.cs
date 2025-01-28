using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;


public class State
{
    public List<AsyncServerStreamingCall<CounterResponse>> _Flux { get; set; }

    public State() { }

    
    public State(IEnumerable<AsyncServerStreamingCall<CounterResponse>> update)
    {
        _Flux = new List<AsyncServerStreamingCall<CounterResponse>>();
    }
}

public class CountFeature : Feature<State>
{
    public override string GetName() => "Count";
    protected override State GetInitialState() => new State();

}