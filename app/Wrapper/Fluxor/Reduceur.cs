using Fluxor;
using grpc;
using Grpc.Core;
using System.Diagnostics;

namespace app.Wrapper.Fluxor;

public static class Reduceur
{
    [ReducerMethod]
    public static State ExecuteState(State state, ActionOutput output)
    {
        var newState = new State(output.Response);
        
        ///<summary>
        ///Forcer Blazor à réagir à l'appel du StateChanged
        ///</summary>
        newState.NotifyStateChanged();

        return newState;
    }
}







