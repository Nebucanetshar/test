using app.Wrapper.Fluxor;
using app.Components;
using Fluxor;
using grpc;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Grpc.Core;


public class AppProgram
{
    public static void Main (string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        

        // ajout du client grpc dans le conteneur de service blazor 
        builder.Services.AddGrpcClient<Counter.CounterClient>(o =>
         {
             o.Address = new Uri("http://localhost:7226");
         });

        ///< summary >
        /// configuration du canal grpc - web activé pour Fluxor(avec addScoped)
        ///</ summary >
        //builder.Services.AddSingleton<GrpcChannel>(GrpcChannel.ForAddress("http://localhost:7226"));
        builder.Services.AddFluxor(o =>
        {
            o.ScanAssemblies(typeof(AppProgram).Assembly); //permet de trouver automatiquement les reducers, effects ect..
            o.UseReduxDevTools(); // permet d'utiliser les outils de developpement Redux

        });

        //configuration du TLS 
        //builder.WebHost.ConfigureKestrel(options =>
        //{
        //    options.ListenLocalhost(5269, listenOptions =>
        //    {
        //        listenOptions.UseHttps("certificat.pfx", "A1996b4860150*");
        //        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
        //    });
        //});

        //builder.Services.AddScoped<IGrpcClient, CounterServer>();
        //builder.Services.AddScoped<IFeatures<State>, Feature>();
        
        ///<summary>
        ///utilisation et configuration du grpc standard 
        ///</summary>
        builder.Services.AddScoped(o =>
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7226")
            };

            var gRpcChannel = GrpcChannel.ForAddress(httpClient.BaseAddress, new GrpcChannelOptions
            {
                HttpClient = httpClient,
                LoggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug))
            });

            return new Counter.CounterClient(gRpcChannel);
        });

        ///<summary>
        ///grpc web activé sans canal, accepte un délégué qui retourne un HttpMessageHandler (encapsulation des appels)
        ///</summary>
        // .ConfigurePrimaryHttpMessageHandler(() =>
        //{
        //    return new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
        //});

        ///<summary>
        ///configuration du canal grpc-web activé(avec un singleton)
        ///</summary> 
        //builder.Services.AddSingleton(services =>
        //{
        //    // obtention de l'url de blazor
        //    var navigation = services.GetRequiredService<IConfiguration>();

        //    var baseUrl = navigation["http://localhost:5269"];

        //    var grpcChannel = GrpcChannel.ForAddress(baseUrl); //?

        //    return new Counter.CounterClient(grpcChannel);
        //});

        ///< summary >
        /// configuration du canal grpc - web activé(avec addScoped)
        ///</ summary >
        //builder.Services.AddScoped(services =>
        //{
        //    var navigation = services.GetRequiredService<NavigationManager>();

        //    var baseUrl = navigation.BaseUri;

        //    var httpClientHandler = new HttpClientHandler();

        //    var grpcChannel = GrpcChannel.ForAddress(baseUrl, new GrpcChannelOptions
        //    {
        //        //journal d'observation requête
        //        LoggerFactory = LoggerFactory.Create(builder => builder.AddConsole()),

        //        HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, httpClientHandler)
        //    });

        //    return new Counter.CounterClient(grpcChannel);
        //});



        #region Programme généré 
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
        #endregion
    }
}

