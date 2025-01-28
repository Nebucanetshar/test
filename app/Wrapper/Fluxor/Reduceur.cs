using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;

public static class Reduceur
{

    [ReducerMethod]
    public static CountState ReduceStartCountingAction(CountState state, StartAction action)
        => state with
        {
            IsCounting = true,
            CurrentCount = action.StartValue
        };
    
    [ReducerMethod]
    public static CountState ReduceCounterUpdateAction(CountState state, UpdateCount action)
        => state with
        {
            CurrentCount = action.NewCount 
        };
}
 





    
