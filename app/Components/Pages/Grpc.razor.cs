
using app.Wrapper.Fluxor;
using Fluxor;
using Microsoft.AspNetCore.Components;
using grpc;
using System.Diagnostics;

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
        ///<summary>
        ///Si on n'inialise pas le store, l'effet ne réagira pas aux actions dispatcher
        ///</summary>
        store.InitializeAsync();
        Trace.TraceInformation("Store Fluxor bien initialisé");

        ///<summary>
        ///Branchement du StateChanged pour que Blazor réagit imparablement 
        ///</summary>
        State.StateChanged += async (sender, args) =>
        {
            await InvokeAsync(StateHasChanged);
            Trace.TraceInformation("UI mise à jour sans erreur !");
        };
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
    }

}
