using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;

public static class Reduceur 
{
    [ReducerMethod]
    public static State ExecuteState(State state, ActionOutput action)
    {
        var updateResponses = new List<AsyncServerStreamingCall<CounterResponse>>(state.Response)
        {
            action.Response
        };
        return state with
        {
            Response = updateResponses
        };
         
    }
}
