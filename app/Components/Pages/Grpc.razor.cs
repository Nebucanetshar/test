
using grpc;
using Grpc.Core;
using Microsoft.AspNetCore.Components;

namespace app.Components.Pages;

public partial class Grpc 
{
    private int currentCount = 0;
    private CancellationToken? cts;

    [Inject]
    public Counter.CounterClient client {  get; set; }

    private async Task CallBroadcast()
    {
        cts = new CancellationToken();

        var request = new CounterRequest { Start = currentCount };
        var response = client.StartCounter(request);

        try
        {
            await foreach (var message in response.ResponseStream.ReadAllAsync())
            {
                currentCount = message.Count;
                StateHasChanged();
            }
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled) { }
    }

    private void StopCount()
    {
        cts = null;
    }
}
