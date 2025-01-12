
using app.Wrapper.Fluxor;
using Fluxor;
using grpc;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;


namespace app.Components.Pages;

public partial class Grpc 
{

    [Inject]
    public IDispatcher dispatcher { get; set; }
    public CounterRequest Request = new CounterRequest { Start = 0 };
    public CancellationTokenSource Cancellation = new CancellationTokenSource();

    public Grpc() { }


    public void Cliked()
    {
        var request = new ActionInput(Request);
        ///<summary>
        ///respect du paradigme Fluxor pour l'exécution de l'effet de manière cohérante 
        ///</summary> 
        dispatcher.Dispatch(request);
    }

    public void Stop()
    {
        Cancellation.Cancel();
    }
}
