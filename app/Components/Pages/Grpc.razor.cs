using grpc.Services;
using grpc;
using Google.Api;
using Grpc.Core;
using Grpc.Net.Client;
using static Google.Rpc.Help.Types;

namespace app.Components.Pages;

public partial class Grpc
{
    private string? responseMessage;
    private readonly Merge.MergeClient ?_mergeClient;

    // options 2 injection dur services via le contructeur
    
    //public Grpc(Merge.MergeClient mergeClient)
    //{
    //    _mergeClient = mergeClient;

    //}

    private async Task CallBroadcast()
    {

        var channel = GrpcChannel.ForAddress("https://localhost:5285");
        var client = new Merge.MergeClient(channel);

        var request = new CounterRequest { Count = 1 };
        var response = client.SayHelloStream(request);

        //responseMessage = response.Message;

    }
}