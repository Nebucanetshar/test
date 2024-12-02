
using app.Wrapper.Fluxor;
using Fluxor;
using grpc;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;


namespace app.Components.Pages;

public partial class Grpc 
{
    [Inject]
    private IDispatcher dispatcher { get; set; }
    public CounterRequest Request = new CounterRequest { Start = 0 };

    public Grpc() { }

    public async Task Cliked()
    {
        var request = new ActionInput(Request);
        dispatcher.Dispatch(request);
    }


}
