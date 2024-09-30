//using grpc.Services;
//using grpc;

//namespace app.Components.Pages;

//public partial class Grpc
//{
//    private string? responseMessage;

//    private async Task CallBroadcast()
//    {
//        var request = new CounterRequest { Count = 1 };
//        CounterResponse response = await Greeter.GreeterBase.SayHelloStream(request, responseMessage);

//        responseMessage = response.Message;

//    }
//}