
using grpc;
using Grpc.Core;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using Microsoft.JSInterop;


namespace app.Components.Pages;

public partial class Grpc : ComponentBase
{ 
    private CancellationToken? cts;
    private CounterResponse ResponseMessage;
    private AsyncServerStreamingCall<CounterResponse> _inner;
    public IAsyncStreamReader<CounterResponse> ResponseStream => _inner.ResponseStream;

    [Inject]
    public Counter.CounterClient client { get; set; }
    
    [Inject]
    public IJSRuntime runtime { get; set; }

    public Grpc() { }

    #region SetCallBroadcastWithoutButton
    //protected override async Task OnInitializedAsync()
    //{
    //    Trace.TraceInformation("Le composant a terminer son initialisation et l'écoute est en cours");
    //    await CallBroadcast();
    //}
    #endregion


    #region JSManagement 
    private async Task TriggerJsFunction()
    {
       await runtime.InvokeVoidAsync("InvokeDotnetMethod");
    }
    
    [JSInvokable]
    public static Task<string> JsInvokabled()
    {
        return Task.FromResult("Hello from.Net");
    }
    #endregion


    #region CallBroadcast
    private async Task CallBroadcast()
    {
        try
        { 
            var request = new CounterRequest { Start = 1 };
            
            var response = client.StartCounter(request);

            while (await response.ResponseStream.MoveNext(CancellationToken.None))
            {
                ResponseMessage = ResponseStream.Current;
            }

        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled) { }
    }

    private void StopCount()
    {
        cts = null;
    }
    #endregion
}
