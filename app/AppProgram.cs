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
using app;



public class AppProgram
{
    public static void Main (string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        ///<summary>
        /// ajout du client grpc dans le conteneur de service blazor 
        ///</summary>
        services.AddGrpcClient<Counter.CounterClient>(o =>
         {
             o.Address = new Uri("http://localhost:7226");
         });


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
        ///Chargement manuelle du ScanAssemblie en constatant les types scannée 
        ///</summary>
        var effetAssembly = typeof(Effet).Assembly;
        services.AddFluxor(o => o.ScanAssemblies(effetAssembly));

        var types = effetAssembly.GetTypes();
        foreach (var type in types)
        {
            Trace.TraceInformation($"Les paradigmes scannée sont: {type.FullName}");
        }

        ///<summary>
        ///Enregistrement manuelle du client dans le conteneur de service AddScoped 
        ///</summary>
        services.AddScoped<ClientFactory>();
        Trace.TraceInformation($"client a était enregistrer en Scoped");

        ///<summary> 
        ///Enregistrement manuelle de l'effet dans le conteneur de service AddScoped 
        ///</summary>
        services.AddScoped<Effet>();
        Trace.TraceInformation("Effet à était enregistrer en Scoped");

        ///<summary>
        ///S'assuré que AddScoped soit definie avant car BuildServiceProvider fige la configuration des services 
        ///donc tout ajout après ne seront pas pris en compte 
        ///</summary>
        var provider = services.BuildServiceProvider();
        var scope = provider.CreateScope();
        
        ///<summary>
        ///Affiche l'enregistrement du services souhaité 
        ///</summary>
        //var register = provider;
        //foreach (var service in register.GetServices<Effet>())
        //{
        //    Trace.TraceInformation($"Le services enregistrer par le scan est : {service.GetType().FullName}");
        //}

        var client = provider.GetService<Counter.CounterClient>();
        if (client != null)
            Trace.TraceInformation("Client gRpc a était inject");
        else
            Trace.TraceInformation("Client est null");

        ///<summary>
        ///Exception levé pour non enregistrement du service dans le conteneur évitant que GetRequiredService 
        ///soit appeler dans un context singleton si Effet est Scoped
        ///</summary>
        //var effet = provider.GetRequiredService<Effet>();
        //var effet = scope.ServiceProvider.GetRequiredService<Effet>();


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

