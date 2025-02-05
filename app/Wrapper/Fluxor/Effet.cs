using Fluxor;
using Grpc.Core;
using grpc;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace app.Wrapper.Fluxor;


public class Effet : ComponentBase
{
    public AsyncServerStreamingCall<CounterResponse> _inner;
    public IAsyncStreamReader<CounterResponse> ResponseStream => _inner.ResponseStream;
    public CounterResponse ResponseMessage;
    public int currentCount = 0;
    private readonly Counter.CounterClient _client;
    private readonly ClientFactory _clientFactory;
    public readonly State _state = new State();

    public Effet(ClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _client = _clientFactory.CreateClient();

        Trace.TraceInformation("client gRpc injecté dans l'effet");
    }

    [EffectMethod]

    public async Task CallBroadcast(ActionInput action, IDispatcher dispatcher)
    {
        _inner = _client.StartCounter(action.Request);

        try
        {
            while (await _inner.ResponseStream.MoveNext(action.Cancellation.Token))
            {
                ResponseMessage = ResponseStream.Current;

                dispatcher.Dispatch(new ActionOutput(_inner));
            }
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            Trace.TraceInformation($"Stream gRpc annulé proprement: {ex.Status.Detail}");
        }
        catch (Exception ex)
        {
            Trace.TraceInformation($"ERREUR global lors de l'effet: {ex.Message}");
        }
    }
}

   
