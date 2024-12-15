using app.Components;
using app.Components.Pages;
using grpc;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.StaticFiles;

public class AppProgram
{
    public static void Main (string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        

        // ajout du client grpc dans le conteneur de service blazor 
        builder.Services.AddGrpcClient<Counter.CounterClient>(o =>
        {
            o.Address = new Uri("http://localhost:5269");
        });

        //// configuration du canal grpc-web activé(avec un singleton)
        //builder.Services.AddSingleton(services =>
        //{
        //    // obtention de l'url de blazor
        //    var navigation = services.GetRequiredService<IConfiguration>();

        //    var baseUrl = navigation["http://localhost:5269"];

        //    var grpcChannel = GrpcChannel.ForAddress(baseUrl); //?

        //    return new Counter.CounterClient(grpcChannel);
        //});

        // configuration du canal grpc-web activé(avec addScoped)
        builder.Services.AddScoped(services =>
        {
            var navigation = services.GetRequiredService<NavigationManager>();

            var baseUrl = navigation.BaseUri;

            var httpClientHandler = new HttpClientHandler();

            var grpcChannel = GrpcChannel.ForAddress(baseUrl, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, httpClientHandler)
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

        app.MapRazorComponents<App>();


        app.Run();

    }
    #endregion
}

