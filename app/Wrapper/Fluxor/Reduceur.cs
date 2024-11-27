using Fluxor;

namespace app;

public static class Reduceur
{
    [ReducerMethod]
    public static State ExecuteState(State state, ActionOutput action)
    {
        return state with
        {
            response = action.ResponseServer
        };
    }
}
