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
    #region test unitaire
    public ActionInput() { }
    #endregion

}

public class ActionOutput
{
    public GrpcFlux Response { get; }
    
    public ActionOutput(GrpcFlux response)
    {
        Response = response;
    }
}
