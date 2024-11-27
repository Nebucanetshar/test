using Fluxor;

namespace app;

[FeatureState]
public record class State
{
    public ResultResponseViewModel? response { get; set; }
    public int data { get; private set; }

    public State()
    {
        response = new ResultResponseViewModel(data);
    }

}
