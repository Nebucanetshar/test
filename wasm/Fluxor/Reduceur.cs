using Fluxor;
namespace wasm.fluxor;

public class Reduceur
{
    [ReducerMethod]
    public static CounterState ExecuteState(CounterState state, ActionOutput action)
    {
        return state with
        {
            response = action.ResponseServer
        };
    }
}
