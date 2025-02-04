using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;


public class State
{
    public AsyncServerStreamingCall<CounterResponse> _Flux;
    public event EventHandler StateChanged;

    public State() { }
    public State(AsyncServerStreamingCall<CounterResponse>update)
    {
        _Flux = update;
    }
    ///<summary>
    ///Fluxor ne déclenche pas automatiquement StateChanged alors on le définie manuellement
    ///directement dans le state
    ///</summary>
    public void NotifyStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}

public class Feature : Feature<State>
{
    public override string GetName() => "Count";
    protected override State GetInitialState() => new State();

}