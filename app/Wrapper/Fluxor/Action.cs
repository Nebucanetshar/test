using grpc;
using Grpc.Core;
using System.Runtime.CompilerServices;

namespace app;

public class ActionInput 
{
    
    public CounterRequest Request = new CounterRequest { Start = 0 };

    public ActionInput(CounterRequest request)
    {
        Request = request;
    }
}

public class ActionOutput
{
    public CounterResponse Response { get; }
    
    public ActionOutput(CounterResponse response)
    {
        Response = response;
    }
}
