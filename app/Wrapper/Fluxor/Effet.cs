using Fluxor;
using Grpc.Core;
using grpc;
using System.Diagnostics;

namespace app.Wrapper.Fluxor;


public class Effet 
{
    public AsyncServerStreamingCall<CounterResponse> _inner;
    public IAsyncStreamReader<CounterResponse> ResponseStream => _inner.ResponseStream;
    private readonly Counter.CounterClient _client;
    private readonly ClientFactory _clientFactory;
    private bool _IsEffectRunning = false;
    
    public Effet(ClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _client = _clientFactory.CreateClient();

        Trace.TraceInformation("client gRpc injecté dans l'effet");
    }

    [EffectMethod]
    public async Task CallBroadcast(ActionOutput action, IDispatcher dispatcher)
    {
        _inner = _client.StartCounter(action._request);

        Trace.TraceInformation($"mise en fonction du token avant foreach: {action._cancellation.Token.IsCancellationRequested}");
        try
        {
            await foreach (var stream in _inner.ResponseStream.ReadAllAsync(action._cancellation.Token))
            {
                dispatcher.Dispatch(new ActionInput(stream));
            }
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            Trace.TraceInformation($"requête annulé: {ex.Status.Detail}");
        }
        
        Trace.TraceInformation($"mise en fonction du token après catch RpcException: {action._cancellation.Token.IsCancellationRequested}");
    }// cette accolade renvoie la condition Cancellation.IsRequested à [false]
}