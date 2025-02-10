
using app.Wrapper.Fluxor;
using Fluxor;
using Microsoft.AspNetCore.Components;
using grpc;
using System.Diagnostics;

namespace app.Components.Pages;

public partial class Grpc : ComponentBase
{
    public CounterRequest _request;

    [Inject]
    public IDispatcher dispatcher { get; set; }
    [Inject]
    public IStore store { get; set; }

    public Grpc()
    {
        _request = new CounterRequest { Start = 0 };
    }
    #region Initialized et StateHasChanged
    protected override void OnInitialized()
    {
        store.InitializeAsync();
        State.StateChanged += OnStateChanged;
    }
    private async void OnStateChanged(object? sender, EventArgs e)
    {
        await InvokeAsync(StateHasChanged);
    }
    #endregion
    public void Call()
    {
        var send = new CallAction(_request);
        dispatcher.Dispatch(send);

        Trace.TraceInformation("requête envoyé");
    }
    public void Stop()
    {
        var cancel = new StopAction();
        dispatcher.Dispatch(cancel);

        Trace.TraceInformation("annulation du stream");
    }
}