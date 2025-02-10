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
    public Effet() { }
    public Effet(ClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _client = _clientFactory.CreateClient();

        Trace.TraceInformation("client gRpc injecté dans l'effet");
    }

    [EffectMethod]
    public async Task CallBroadcast(CallAction action, IDispatcher dispatcher)
    {
        _inner = _client.StartCounter(action._request);
        
        await foreach (var stream in _inner.ResponseStream.ReadAllAsync())
        {
            dispatcher.Dispatch(new InputAction(stream));
        }
    }

    [EffectMethod]
    public Task StopBroadcast(StopAction action,IDispatcher dispatcher)
    {
       if (_inner != null)
       {
            _inner.Dispose();
       }
        return Task.CompletedTask;
    }
}