using Microsoft.AspNetCore.Components;
using grpc;

namespace app;

public interface IgrpcCounterServiceClient
{
    [Inject]
    public Counter.CounterClient client { get; set; }
    Task<ResponseWrapperViewModel<Counter.CounterClient>> StarCounter(CounterRequest request);
}
