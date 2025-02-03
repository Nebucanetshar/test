using Fluxor;
using Grpc.Core;
using grpc;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace app.Wrapper.Fluxor;


public class Effet : Effect<ActionInput>
{
    public CancellationTokenSource Cancellation = new CancellationTokenSource();

    public AsyncServerStreamingCall<CounterResponse> _inner;
    public IAsyncStreamReader<CounterResponse> ResponseStream => _inner.ResponseStream;
    public CounterResponse ResponseMessage;
    
    private readonly Counter.CounterClient _client;
    private readonly ClientFactory _clientFactory;

    //[Inject]
    //public Counter.CounterClient _client { get; set; }
    public Effet(ClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _client = _clientFactory.CreateClient();
        
        Trace.TraceInformation("client gRpc injecté dans l'effet");
    }

    [EffectMethod]
    public override async Task HandleAsync(ActionInput action, IDispatcher dispatcher)
    {
        _inner = _client.StartCounter(action.Request);

        try
        {
            while (await _inner.ResponseStream.MoveNext(Cancellation.Token))
            {
                ResponseMessage = ResponseStream.Current;
            }

            dispatcher.Dispatch(new ActionOutput(_inner));

        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled) { }
        
    }

   
}

   
