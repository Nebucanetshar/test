
using grpc;
using Grpc.Core;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using Microsoft.JSInterop;


namespace app.Components.Pages;

public partial class Grpc : ComponentBase
{
    public int CurrentCount = 0;
    
    private bool _isRunning = false;
    private CancellationTokenSource? _cancellation;
    
    private CounterResponse ResponseMessage;
    public AsyncServerStreamingCall<CounterResponse> _inner;
    public IAsyncStreamReader<CounterResponse> ResponseStream => _inner.ResponseStream;

    [Inject]
    public Counter.CounterClient client { get; set; }

    //[Inject]
    //public IJSRuntime runtime { get; set; }

    public Grpc() { }
    
    #region JSManagement 
    //private async Task TriggerJsFunction()
    //{
    //    await runtime.InvokeVoidAsync("invokeDotnetMethod");
    //}

    //[JSInvokable("CallBroadcastJs")]
    //public static Task<string> CallBroadcastJs()
    //{
    //    return Task.FromResult("Hello from.Net");
    //}
    #endregion


    #region CallBroadcast
    public async Task CallBroadcast()
    {
        _isRunning = true;
        _cancellation = new CancellationTokenSource();
        var token = _cancellation.Token;
       
        try
        {
            var request = new CounterRequest { Start = CurrentCount };
            var response = client.StartCounter(request);

            await foreach (var broadcast in response.ResponseStream.ReadAllAsync().WithCancellation(token))
            {
                CurrentCount = broadcast.Count;
                StateHasChanged();
            }

            //while (await response.ResponseStream.MoveNext(CancellationToken.None))
            //{
            //    ResponseMessage = ResponseStream.Current;
            //}
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled) { }
    }

    public void StopCount()
    {
        if (_isRunning && _cancellation != null)
        {
            _cancellation.Cancel();
        }
    }
        
    #endregion
}
