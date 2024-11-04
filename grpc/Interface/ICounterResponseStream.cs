using Grpc.Core;
namespace grpc;

public interface ICounterResponseStream
{
    IAsyncStreamReader<CounterResponse> ResponseStream
    {
        get
        {
            return ResponseStream;
        }
    }
}
