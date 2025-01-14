using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;

public static class Reduceur
{
    [ReducerMethod]
    public static State ExecuteState(State state, ActionOutput action)
    {
        var updateResponses = new List<AsyncServerStreamingCall<CounterResponse>>(state._Flux)
        {
            action.Response
        };
        // renvoie un nouvel état avec la liste mis a jour 
        return new State
        {
            _Flux = updateResponses
        };
    }
}

        //}
        //[ReducerMethod]
        //public class MessageReduceur : Reducer<State,ActionOutput>
        //{
        //    public override State Reduce(State state, ActionOutput action)
        //    {
        //        var updateMessages = state._Flux.Append(action.Response);
        //        return new State(updateMessages);
        //    }
        //}





    
