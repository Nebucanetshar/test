using grpc;
using Grpc.Core;


namespace app.Wrapper.Fluxor;


public class ActionInput
{
    public CounterRequest Request { get; set; }

    public ActionInput(CounterRequest request)
    {
        Request = request;
    }
}

public class ActionOutput
{
    public AsyncServerStreamingCall<CounterResponse> Response { get; }

    public ActionOutput(AsyncServerStreamingCall<CounterResponse> response)
    {
        Response = response;
    }
}

