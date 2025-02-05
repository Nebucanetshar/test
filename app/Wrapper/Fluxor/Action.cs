using grpc;
using Grpc.Core;
using System.Text.Json.Serialization;


namespace app.Wrapper.Fluxor;


public class ActionInput
{
    public CounterRequest Request = new CounterRequest { Start = 0 };
    public CancellationTokenSource Cancellation = new CancellationTokenSource();

    public ActionInput(CounterRequest request)
    {
        Request = request;
    }
    public ActionInput (CancellationTokenSource cancel)
    {
        Cancellation.Cancel();
    }
}

[JsonConverter(typeof(CounterResponseJsonConverter))]
public class ActionOutput
{
    public AsyncServerStreamingCall<CounterResponse> Response { get; }

    public ActionOutput(AsyncServerStreamingCall<CounterResponse> response)
    {
        Response = response;
    }
}