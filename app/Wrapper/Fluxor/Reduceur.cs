using Fluxor;

namespace app.Wrapper.Fluxor;

public static class Reduceur
{
    [ReducerMethod]
    public static State CallState(State state, ActionInput intput)
    { 
        return new State(intput._response);
    }

    [ReducerMethod]
    public static State StopState(State state, ActionOutput stop)
    {
        return new State(stop._cancellation);
    }
}