using grpc;
using Grpc.Core;
using System.Runtime.CompilerServices;

namespace app.Wrapper.Fluxor;

public class ActionInput 
{
    public CounterRequest request { get; set; }

    public ActionInput(CounterRequest request)
    {
        this.request = request;
    }

    public ActionInput() { }

}

public class ActionOutput
{
    public AsyncServerStreamingCall<CounterResponse> Response { get; }
    
    public ActionOutput(AsyncServerStreamingCall<CounterResponse> response)
    {
        Response = response;
    }
}
