using Fluxor;
using grpc;

namespace app.Wrapper.Fluxor;

public class State
{
    public CounterResponse? _response { get; }
    #region test unitaire
    public int test { get; set; }

    public State(int test)
    {
        this.test = test;
    }
    #endregion
    public State(CounterResponse? response = null)
    {
        _response = response;
    }
}

#region Feature
public class Feature : Feature<State>
{
    public override string GetName() => "Count";
    protected override State GetInitialState() => new State();
}
#endregion