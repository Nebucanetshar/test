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
        return new State(output.Response);
    }
}







