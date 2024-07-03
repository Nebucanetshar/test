using Fluxor;
namespace wasm.fluxor;

[FeatureState]
public record class State
{
    public ResultResponseViewModel? response { get; set; }
    public string data { get; private set; }

    public CounterState()
    {
        response = new ResultResponseViewModel(data);
    }
}
