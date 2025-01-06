using app.Components;
using grpc;
using grpc.Services;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Syncfusion.Blazor;


public class AppProgram
{
    public static void Main (string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

        // ajout du client grpc dans le conteneur de service blazor 
        builder.Services.AddGrpcClient<Counter.CounterClient>(o =>
        {
            o.Address = new Uri("https://localhost:7226");
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
        });


        // configuration du canal grpc-web (avec addScoped)
        builder.Services.AddScoped(services =>
        {
            var navigation = services.GetRequiredService<NavigationManager>();

            var baseUrl = navigation.BaseUri;

            var httpClientHandler = new HttpClientHandler();

            var grpcChannel = GrpcChannel.ForAddress(baseUrl, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, httpClientHandler),
                LoggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug))
            });

            return new Counter.CounterClient(grpcChannel);
        });



        #region program généré
        // Add services to the container.
        builder.Services.AddRazorComponents().AddInteractiveServerComponents();
        builder.Services.AddSyncfusionBlazor();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
        app.Run();
    }
    #endregion
}

