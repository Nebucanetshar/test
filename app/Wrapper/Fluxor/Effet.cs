using Fluxor;
using Grpc.Core;
using grpc;
using Microsoft.AspNetCore.Components;

namespace app.Wrapper.Fluxor;

public class Effet 
{
    public CancellationTokenSource Cancellation = new CancellationTokenSource();
    
    public AsyncServerStreamingCall<CounterResponse> _inner;
    public IAsyncStreamReader<CounterResponse> ResponseStream => _inner.ResponseStream;
    public CounterResponse ResponseMessage;

    [Inject]
    public Counter.CounterClient client { get; set; }

    public Effet() { }


    [EffectMethod]
    public async Task CallBroadcast(StartAction action, IDispatcher dispatcher)
    {
        var request = new CounterRequest { Start = action.StartValue };
        try
        {
            _inner = client.StartCounter(request);
            
          
            while(await _inner.ResponseStream.MoveNext(Cancellation.Token))
            {
                ResponseMessage = ResponseStream.Current;
            }

            dispatcher.Dispatch(new UpdateCount(_inner)); 

        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled) { }
    }

   
}
