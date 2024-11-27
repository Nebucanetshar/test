using Fluxor;
using Grpc.Core;
using grpc;
using Microsoft.AspNetCore.Components;

namespace app;

public class Effet
{

    private IgrpcCounterServiceClient _grpcCounterServiceClient;

    

    public Effet(IgrpcCounterServiceClient server)
    {
        _grpcCounterServiceClient = server;
    }


    [EffectMethod]
    public async Task CallBroadcast(ActionInput action, IDispatcher dispatcher)
    {
        try
        {
            var response = await _grpcCounterServiceClient.StarCounter(action.Request);
            dispatcher.Dispatch(new ActionOutput(response.Content));

        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled) { }
    }
}
