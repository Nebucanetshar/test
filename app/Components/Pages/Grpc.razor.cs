
using app.Wrapper.Fluxor;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace app.Components.Pages;

public partial class Grpc 
{
    CancellationTokenSource Cancellation = new CancellationTokenSource();
    
    [Inject]
    public IDispatcher dispatcher { get; set; }


    public Grpc() { }
    public void Cliked()
    {
        dispatcher.Dispatch(new StartAction(0));
    }

    public void Stop()
    {
        Cancellation.Cancel();
    }
}
