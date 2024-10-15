using app.Components;
using app.Components.Layout;
using grpc;
using Syncfusion.Blazor;
using grpc.Services;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;

public class AppProgram
{
    public static void Main (string[] args)
    {
        // ajout de l'enregistrement du client grpc dans conteneur service blazor

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpcClient<Merge.MergeClient>(o =>
        {
            o.Address = new Uri("https://localhost:7091");
        });

        //ignore les erreurs de certificat auto-signé

        //.ConfigurePrimaryHttpMessageHandler(() =>
        //{
        //    return new HttpClientHandler
        //    {
        //        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        //    };
        //});

        builder.Services.AddRazorComponents().AddInteractiveServerComponents();
        builder.Services.AddSyncfusionBlazor();

        
        builder.Services.AddScoped(services =>
        {
            //obtenir l'url de l'application 
            var navigationManager = services.GetRequiredService<NavigationManager>();
            
            //donne l'url de base de l'application 
            var baseUrl = navigationManager.BaseUri;

            //configuration du client grpc-Web
            var httpClientHandler = new HttpClientHandler();

            //configuration d'un canal grpc avec grpc-Web activé
            var grpcWebChannel = GrpcChannel.ForAddress(baseUrl, new GrpcChannelOptions
            {
                //utilisation de grpc-web
                HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, httpClientHandler)
            });
            //retour du client grpc crée 
            return new Merge.MergeClient(grpcWebChannel);
        });

            ///<summary>Middleware Blazor App</summary>
        var app = builder.Build();

       
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        

        app.Run();

    }
}


