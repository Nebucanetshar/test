
//using Fluxor;
//using grpc;
//using Grpc.Core;
//using Microsoft.AspNetCore.Components;
//using System.Diagnostics;


//namespace app.Components.Pages;

//public partial class Grpc : ComponentBase, ICounterResponseStream
//{
//    public int currentCount = 0;
//    private CancellationToken? cts;
//    private CounterResponse ResponseMessage;
//    public AsyncServerStreamingCall<CounterResponse> _inner;
//    public IAsyncStreamReader<CounterResponse> ResponseStream => _inner.ResponseStream;

//    [Inject]
//    public Counter.CounterClient client { get; set; }


//    public Grpc() { }



//    #region test unitaire

//    public IClient Object { get; }
//    /// <summary>
//    /// pour la simulation du client avec Moq
//    /// </summary>
//    /// <param name="currentCount"></param>
//    /// <param name="cts"></param>
//    /// <param name="client"></param>
//    /// <param name="channel"></param>
//    public Grpc(int currentCount, CancellationToken? cts, Counter.CounterClient client, Counter.CounterClient channel)
//    {
//        this.currentCount = currentCount;
//        this.cts = cts;
//        this.client = client;
//        this.channel = channel;
//    }

//    public Grpc(IClient @object)
//    {
//        Object = @object;
//    }
//    #endregion

//    protected override async Task OnInitializedAsync()
//    {
//        Trace.TraceInformation("Le composant a terminer son initialisation et l'écoute est en cours");
//        await CallBroadcast();

//    }

//    public async Task CallBroadcast()
//    {
//        try
//        {
//            cts = new CancellationToken();

//            if (cts != null)
//            {
//                var request = new CounterRequest
//                {
//                    Start = currentCount
//                };

//                var response = client.StartCounter(request);

//                //await foreach (var message in response.ResponseStream.ReadAllAsync())
//                //{
//                //    currentCount = message.Count;
//                //    StateHasChanged();
//                //}

//                while (await response.ResponseStream.MoveNext(CancellationToken.None))
//                {
//                    ResponseMessage = ResponseStream.Current;
//                }
//            }
//        }
//        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled) { }
//    }

//    private void StopCount()
//    {
//        cts = null;
//    }

//}
