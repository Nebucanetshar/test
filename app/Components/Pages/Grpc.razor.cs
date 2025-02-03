
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
        var send = new ActionInput(Request);
        dispatcher.Dispatch(send);

        Trace.TraceInformation("requête envoyé");
    }
    public void Stop()
    {
        var cancel = new ActionInput(Cancellation);
        dispatcher.Dispatch(cancel);

        Trace.TraceInformation("requête annulé");
    }

}
