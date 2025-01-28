using grpc;
using Grpc.Core;


namespace app.Wrapper.Fluxor;

public record StartAction(int StartValue);

public class UpdateCount
{
    public AsyncServerStreamingCall<CounterResponse> _inner;
    public int NewCount;

    public UpdateCount(AsyncServerStreamingCall<CounterResponse> inner) 
    {
        _inner = inner;
    }
}

