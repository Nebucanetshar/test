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

    private async Task CallBroadcast()
    {
        //*********** create canal and grpcClient **************

        var channel = GrpcChannel.ForAddress("https://localhost:5285");
        var client = new Merge.MergeClient(channel);
       

        var request = new CounterRequest { Count = 1 };
        var call = client.SayHelloStream(request);

        var response = new CounterResponse();
        
        responseMessage = response.Message;

    }
}