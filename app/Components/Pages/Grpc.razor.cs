using app.Components.Pages;
using grpc;
using Microsoft.AspNetCore.Components;

namespace app.Component.Page;

public partial class Grpc
{
    private string Response;

    [Inject]
    public Counter.CounterClient client { get; set; }

    public Grpc() { }

    public override async Task CallBroscast()
    {

    }
}