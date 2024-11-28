using Fluxor;
using Grpc.Core;
using grpc;
using Microsoft.AspNetCore.Components;

namespace app;

public class Effet
{
    public AsyncServerStreamingCall<CounterResponse> _inner;
    public IAsyncStreamReader<CounterResponse> ResponseStream => _inner.ResponseStream;
    public CounterResponse ResponseMessage;

    [Inject]
    public Counter.CounterClient client { get; set; }

    public Effet() { }
    

    [EffectMethod]
    public async Task CallBroadcast(ActionInput action, IDispatcher dispatcher)
    {
        try
        {
            var response = client.StartCounter(action.Request);

            while(await response.ResponseStream.MoveNext(CancellationToken.None))
            {
                ResponseMessage = ResponseStream.Current;
            }
                
            dispatcher.Dispatch(new ActionOutput(response._inner)); // find property asyncServerStreamCall compatible with response

        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled) { }
    }
}
