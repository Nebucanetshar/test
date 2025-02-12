using app.Components;
using Fluxor;
using grpc;
using Grpc.Net.Client;
using Syncfusion.Blazor;
using app;
using Syncfusion.Licensing;

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
        ///Chargement manuelle du ScanAssemblie 
        ///</summary>
        var assembly = typeof(AppProgram).Assembly;
        services.AddFluxor(o => o.ScanAssemblies(assembly));

        ///<summary>
        ///Configuration du canal gRpc avec AddScoped pour Fluxor
        ///</summary>
        services.AddScoped<ClientFactory>(o =>
        {
            var channel = o.GetRequiredService<GrpcChannel>();
            return new ClientFactory(channel);
        });

        ///<summary> 
        ///Enregistrement manuelle du canal dans le conteneur de service Singleton
        ///</summary>
        services.AddSingleton(GrpcChannel.ForAddress("https://localhost:7226"));

        #region test unitaire
        SyncfusionLicenseProvider.RegisterLicense("license key");
        #endregion

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