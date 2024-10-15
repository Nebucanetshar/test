using grpc;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;


namespace app.Components.Pages;

public partial class Grpc
{
    public Merge.MergeClient _mergeClient;
    //private readonly Merge.MergeClient ?_mergeClient;
    private string errorMessage = string.Empty;
    public IAsyncStreamReader<CounterResponse> asyncStreamReader;
    private CounterResponse responseMessage;
    //private string responseMessage = string.Empty;
    [Inject]
    public Merge.MergeClient client {  get; set; }


    // options 2 injection du services via le contructeur

    public Grpc(Merge.MergeClient mergeClient)
    {
        _mergeClient = mergeClient;

    }

    public Grpc() { }
    //public Grpc(IAsyncStreamReader<CounterResponse> stream)
    //{
    //    asyncStreamReader = stream;
    //}

    private async Task CallBroadcast()
    {
        errorMessage =string.Empty;
        try
        {
            var request = new CounterRequest { Count = 1 };
            var response = _mergeClient.SayHelloStream(request);

            //await foreach (var message in response.ResponseStream.ReadAllAsync())
            //{
            //    responseMessage += message.Message + "\n";
            //}

            while (await asyncStreamReader.MoveNext(CancellationToken.None))
            {
                responseMessage = asyncStreamReader.Current;
            }
        }

        catch (RpcException rpcEx)
        {
            errorMessage = $"grpc Error :{ rpcEx.Message}";
        }

        catch (Exception ex)
        {
            errorMessage = $" une erreur s'est produite : {ex.Message}";
        }
    }
}