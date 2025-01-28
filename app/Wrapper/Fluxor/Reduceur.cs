using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;

public static class Reduceur
{
    [ReducerMethod]
    public static State ExecuteState(State state, ActionOutput output)
    {
        var update = new List<AsyncServerStreamingCall<CounterResponse>>(state._Flux)
        {
           output.Response
        };

        return new State
        {
            _Flux = update,
        };
    }
}
 





    
