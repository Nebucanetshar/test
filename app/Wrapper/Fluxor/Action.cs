using grpc;

namespace app.Wrapper.Fluxor;

public class CallAction
{
    public CounterRequest _request;
    
    public CallAction(CounterRequest request)
    {
        _request = new CounterRequest { Start = 0 };
    }
}

public class StopAction { }

public class InputAction
{
    public CounterResponse _response { get; }

    public InputAction(CounterResponse response)
    {
        _response = response;
    }
}