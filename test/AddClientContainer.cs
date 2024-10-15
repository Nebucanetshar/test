
//using grpc;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;

//namespace test;

//public class GrpcClientTest : IClassFixture<WebApplicationFactory<AppProgram>>
//{
//    private readonly WebApplicationFactory<AppProgram> _factory;

//    public GrpcClientTest(WebApplicationFactory<AppProgram> factory)
//    {
//        _factory = factory;
//    }
//    [Fact]
//    public async Task GrpcClient_AddAndConfigurationClient()
//    {
//        //arrange 
//        var Client = _factory.WithWebHostBuilder(builder =>
//        {
//            builder.ConfigureServices(services =>
//            {
//                ////utiliser un TestServer pour simuler un server Grpc
//                //var container =new SimpleInjector.Container();
//                //var mockMergeClient = new Mock<Merge.MergeClient>(MockBehavior.Strict, null);
//                //container.RegisterInstance(mockMergeClient.Object);

//                //configure grpcClient 
//                services.AddGrpcClient<Merge.MergeClient>(o =>
//                {
//                    o.Address = new Uri("https://localhost:7091");
//                });


//            });
//        }).CreateClient();

//        //act 
//        var serviceProvider = _factory.Services;
//        var grpcClient = serviceProvider.GetService<Merge.MergeClient>();

//        //assert 
//        Assert.NotNull(grpcClient);
//        Assert.IsAssignableFrom<Merge.MergeClient>(grpcClient);
//    }
//}
