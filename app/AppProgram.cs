using app.Components;
using app.Components.Layout;
using grpc;
using Syncfusion.Blazor;

public class AppProgram
{
    public static void Main (string[] args)
    {
        // ajout de l'enregistrement du client grpc 

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


