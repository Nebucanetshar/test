
using app.Wrapper.Fluxor;
using Fluxor;
using Grpc.Core;
using Microsoft.AspNetCore.Components;
using grpc;
using System.Diagnostics;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace app.Components.Pages;

public partial class Grpc : ComponentBase
{
    public CancellationTokenSource Cancellation = new CancellationTokenSource();
    public CounterRequest Request = new CounterRequest { Start = 0 };

    [Inject]
    public Counter.CounterClient client { get; set; }

    [Inject]
    public IDispatcher dispatcher { get; set; } 

    public Grpc() { }

   
    public void Cliked()
    {
        var request = new ActionInput(Request);
        dispatcher.Dispatch(request);

        Trace.TraceInformation("l'action a était dispatcher");
    }
    public void Stop()
    {
        Cancellation.Cancel();
    }

}
