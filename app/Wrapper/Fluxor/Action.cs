using grpc;

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
    public ResultResponseViewModel? ResponseServer { get; set; }

    public ActionOutput(ResultResponseViewModel responseServer)
    {
        ResponseServer = responseServer;
        
    }
}
