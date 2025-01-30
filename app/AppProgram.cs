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
using System.Diagnostics;
using app.Components.Pages;



public class AppProgram
{
    public static void Main (string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var provider = services.BuildServiceProvider();


        ///<summary>
        /// ajout du client grpc dans le conteneur de service blazor 
        ///</summary>
        services.AddGrpcClient<Counter.CounterClient>(o =>
         {
             o.Address = new Uri("http://localhost:7226");
         });

        ///<summary> 
        ///enregistrement manuelle de l'effet dans le conteneur de service addScoped 
        ///</summary>
        services.AddScoped<Effet>();
        services.AddScoped<IDispatcher, Fluxor.Dispatcher>();


        ///<summary>
        ///Chargement automatiquement du ScanAssemblie avec les outils de developpement Redux
        ///</summary>
        //services.AddFluxor(o =>
        //{
        //    o.ScanAssemblies(typeof(AppProgram).Assembly);
        //    o.UseReduxDevTools();

        //    Trace.TraceInformation("Scan fluxor effectué");
        //});

        ///<summary>
        ///Chargement manuelle du ScanAssemlie en constatant les types scannée 
        ///</summary>
        var effetAssembly = typeof(Effet).Assembly;
        services.AddFluxor(o => o.ScanAssemblies(effetAssembly));

        Trace.TraceInformation($"Chargement manuelle : {effetAssembly.FullName}");

        var types = effetAssembly.GetTypes();
        foreach (var type in types)
        {
            Trace.TraceInformation($"Les paradigme trouvé sont:{type.FullName}");
        }

        ///<summary>
        ///Affiche l'enregistrement du services souhaité par le ScanAssemblies
        ///</summary>
        //var register = provider;
        //foreach ( var service in register.GetServices<Effet>())
        //{
        //    Trace.TraceInformation($"Les services enregistrer par le scan sont : {service.GetType().FullName}");
        //}

        //var effet = provider.GetRequiredService<Effet>();


        ///<summary>
        ///utilisation et configuration du canal avec grpc standard 
        ///</summary>
        services.AddScoped(o =>
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

