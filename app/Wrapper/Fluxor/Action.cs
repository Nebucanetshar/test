using grpc;

namespace app.Wrapper.Fluxor;

public class ActionOutput
{
    public CounterRequest _request = new CounterRequest { Start = 0 };
    public CancellationTokenSource _cancellation = new CancellationTokenSource();

    public ActionOutput(CounterRequest request)
    {
        _request = request;
    }
    public ActionOutput(CancellationTokenSource cancel)
    {
        _cancellation.Cancel();
    }
}

public class ActionInput
{
    public CounterResponse _response { get; }

    public ActionInput(CounterResponse response)
    {
        _response = response;
    }
}