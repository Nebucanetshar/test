using grpc;
using Grpc.Core;


namespace app.Wrapper.Fluxor;

public class ActionInput 
{
    public CounterRequest request { get; set; }

    public ActionInput(CounterRequest request)
    {
        this.request = request;
    }
    #region test unitaire
    public ActionInput() { }
    #endregion

}

public class ActionOutput
{
    public AsyncServerStreamingCall<CounterResponse> Response { get; }
    
    public ActionOutput(AsyncServerStreamingCall<CounterResponse> response)
    {
        Response = response;
    }
}
