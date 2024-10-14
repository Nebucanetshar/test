using grpc.Services;
using grpc;
using Google.Api;
using Grpc.Core;
using Grpc.Net.Client;
using static Google.Rpc.Help.Types;
using System.Runtime.CompilerServices;

namespace app.Components.Pages;

public partial class Grpc
{
    private readonly Merge.MergeClient ?_mergeClient;
    //private string responseMessage = string.Empty;
    private bool isLoading = false;
    private string errorMessage = string.Empty;
    public IAsyncStreamReader<CounterResponse> asyncStreamReader;
    private CounterResponse responseMessage;
    

    // options 2 injection du services via le contructeur

    //public Grpc(Merge.MergeClient mergeClient)
    //{
    //    _mergeClient = mergeClient;

    //}

    public Grpc() { }
    //public Grpc(IAsyncStreamReader<CounterResponse> stream)
    //{
    //    asyncStreamReader = stream;
    //}

    private async Task CallBroadcast()
    {
        isLoading = true;
        errorMessage =string.Empty;
        try
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7091");
            var client = new Merge.MergeClient(channel);

            var request = new CounterRequest { Count = 1 };
            var response = client.SayHelloStream(request);

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

        finally
        {
            isLoading=false;
            StateHasChanged() ;
        }
    }
}