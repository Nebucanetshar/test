using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;


public class State
{
    public AsyncServerStreamingCall<CounterResponse> _Flux;

    public State() { }
    public State(AsyncServerStreamingCall<CounterResponse>update)
    {
        _Flux = update;
    }

}

public class Feature : Feature<State>
{
    public override string GetName() => "Count";
    protected override State GetInitialState() => new State();

}