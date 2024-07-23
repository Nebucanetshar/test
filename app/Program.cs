using app.Components;
using app;
using Grpc.Net.Client;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorComponents().AddInteractiveServerComponents();

        var app = builder.Build();

        
        if (!app.Environment.IsDevelopment())
        
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();


        app.Run();

        //*********** create canal and grpcClient **************

        var channel = GrpcChannel.ForAddress("https://localhost:7052");
        var client = new Link.LinkClient(channel);

    }
}


