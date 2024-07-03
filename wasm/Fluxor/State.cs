using Fluxor;
using common.ViewModel;
namespace wasm.fluxor;

[FeatureState]
public record class State
{
    public ResultResponseViewModel? response { get; set; }
    public string data { get; private set; }

    public State()
    {
        response = new ResultResponseViewModel(data);
    }
}
