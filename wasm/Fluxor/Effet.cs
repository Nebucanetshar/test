using Microsoft.AspNetCore.Components;
using common.Wrapper;
using Fluxor;

namespace wasm.fluxor;

public class Effet
{
    private IgRpcCounterServiceClient gRpcCounterServiceClient;

    public Effet(IgRpcCounterServiceClient server)
    {
        gRpcCounterServiceClient = server;
    }
    [EffectMethod]
    public async Task ExecuteEffet(ActionInput action, IDispatcher dispatcher)
    {
        var responseWrapper = await gRpcCounterServiceClient.DoCreate(action.Counter);

        dispatcher.Dispatch(new ActionOutput(responseWrapper.Content));
    }
}
