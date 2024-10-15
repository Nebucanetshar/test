using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace test;

public class CreateCanal
{
    

    [Fact]
    public async Task CreateCanal_Open()
    {

        ///<summary>arrange</summary>
        //donne l'url de base de l'application 
        var baseUrl = "https://localhost:7071";
        //configuration du client grpc-Web
        var httpClientHandler = new Mock<HttpClientHandler>();

        ///<summary>Act</summary>
        //configuration d'un canal grpc avec grpc-Web activé
        var grpcWebChannel = GrpcChannel.ForAddress(baseUrl, new GrpcChannelOptions
        {
            //utilisation de grpc-web
            HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, httpClientHandler.Object)
        });
        
        ///<summary>Assert</summary>
           Assert.NotNull(grpcWebChannel);// verifie si le canal a bien été crée 
           Assert.Equal(baseUrl,grpcWebChannel.Target); //verifie que l'url du canal correspond a l'url attendu 

    }

}
