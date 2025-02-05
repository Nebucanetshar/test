using Fluxor;
using grpc;

namespace app.Wrapper.Fluxor;


public class State
{
    public CounterResponse? _response { get; }
    public CancellationTokenSource? _cancellation { get; }

    public State(CounterResponse? response = null)
    {
        _response = response;
    }

    public State(CancellationTokenSource cancellation)
    {
        _cancellation = cancellation;
    }
}

public class Feature : Feature<State>
{
    public override string GetName() => "Count";
    protected override State GetInitialState() => new State();

}