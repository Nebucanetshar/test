using Fluxor;

namespace app.Wrapper.Fluxor;

public static class Reduceur
{
    [ReducerMethod]
    public static State CallState(State state,InputAction intput)
    { 
        return new State(intput._response);
    }
}