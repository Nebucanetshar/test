
using grpc;
using Grpc.Core;
using Microsoft.AspNetCore.Components;

namespace app.Components.Pages;

public partial class Grpc : ComponentBase
{
    private CancellationTokenSource Cancellation = new CancellationTokenSource();
    
    private CounterResponse ResponseMessage;
    public AsyncServerStreamingCall<CounterResponse> _inner;
    public IAsyncStreamReader<CounterResponse> ResponseStream => _inner.ResponseStream;

    [Inject]
    public Counter.CounterClient client { get; set; }

    public Grpc() { }

    #region CallBroadcast
    public async Task CallBroadcast()
    {
        try
        {
            var request = new CounterRequest { Start = 0 };
            _inner = client.StartCounter(request);

            while (await _inner.ResponseStream.MoveNext(Cancellation.Token))
            {
                ResponseMessage = ResponseStream.Current;
                StateHasChanged();
            }
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled) { }
    }

    public void StopCount()
    {
        Cancellation.Cancel();
    }
        
    #endregion
}
