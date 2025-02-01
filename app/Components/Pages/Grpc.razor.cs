
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
    public IDispatcher dispatcher { get; set; }
    [Inject]
    public IStore store { get; set; }

    public Grpc() { }

    protected override void OnInitialized()
    {
        store.InitializeAsync();
        Trace.TraceInformation("Store Fluxor bien initialisé");
    }
    public void Cliked()
    {
        var request = new ActionInput(Request);
        dispatcher.Dispatch(request);

        Trace.TraceInformation("l'action à était dispatcher");
    }
    public void Stop()
    {
        Cancellation.Cancel();
    }

}
