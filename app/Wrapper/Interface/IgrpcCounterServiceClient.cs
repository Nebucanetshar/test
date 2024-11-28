using Microsoft.AspNetCore.Components;
using grpc;
using Grpc.Core;

namespace app;

public interface IgrpcCounterServiceClient
{ 
    
    Task<ResponseWrapperViewModel<Counter.CounterClient>> StartCounter(CounterRequest request);
}
