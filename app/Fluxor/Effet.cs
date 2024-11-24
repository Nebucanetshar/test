using Fluxor;
using Grpc.Core;
using grpc;
using Microsoft.AspNetCore.Components;

namespace app;

public class Effet
{
    public CancellationToken? cts;
    public CounterResponse? ResponseMessage;
    public AsyncServerStreamingCall<CounterResponse> _inner;
    public IAsyncStreamReader<CounterResponse> ResponseStream => _inner.ResponseStream;

    [Inject]
    public Counter.CounterClient client { get; set; }


    [EffectMethod]
    public async Task CallBroadcast(ActionInput action, IDispatcher dispatcher)
    {
        try
        {
            cts = new CancellationToken();

            if (cts != null)
            {
                var response = client.StartCounter(action.Request);

                //await foreach (var message in response.ResponseStream.ReadAllAsync())
                //{
                //    currentCount = message.Count;
                //    StateHasChanged();
                //}

                while (await response.ResponseStream.MoveNext(CancellationToken.None))
                {
                    ResponseMessage = ResponseStream.Current;
                }
            }

        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled) { }
    }

    private void StopCount()
    {
        cts = null;
    }
}
