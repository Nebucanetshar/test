using LinqToDB;
using Microsoft.Extensions.Options;
using app.Wrapper.Fluxor;
using Microsoft.AspNetCore.Builder;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using grpc;

namespace console;

public class Program
{
    public static void Main(string[] args)
    {
        #region supervision de l'activié de AppProgram 
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
        ///chargement manuelle du ScanAssembly Fluxor et observation des types scannées
        ///</summary>

        var assembly = typeof(AppProgram).Assembly;
        services.AddFluxor(o => o.ScanAssemblies(assembly));

        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            Console.WriteLine($"les paradigmes scanné sont : {type.FullName}");
        }

        ///<summary>
        ///enregistrement manuelle dans un conteneur de service AddScoped
        ///</summary>
        services.AddScoped<Effet>();
        Console.WriteLine("effet est enregistrer en AddScoped");

        ///<summary>
        ///affiche l'enregistrement du paradigme Fluxor souhaité et l'ajout du client gRpc dans un conteneur de service,
        ///s'assuré que les conteurs de services soit définie avant BuildServiceProvider car celui ci 
        ///fige la configuration des services, tout ajout après provoquera une exception levé par unhandler
        ///(penser à ajouté un contructeur vide dans le paradigme Fluxor étudié) 
        ///</summary>
        var provider = services.BuildServiceProvider();
        var scope = provider.CreateScope();

        var paradigme = provider.GetServices<Effet>();
        Console.WriteLine($"le paradigme scannée est bien enregistré : {paradigme.GetType().FullName}");

        var client = provider.GetService<Counter.CounterClient>();
        Console.WriteLine( client != null ? "le client gRpc est bien injecté dans app" :"le client gRpc est null");

        ///<summary>
        ///exception levé pour non enregistrement du service dans un conteneur tout en
        ///évitant que GetRequiredService soit appeler dans un context singleton
        ///</summary>
        var unhanled = scope.ServiceProvider.GetRequiredService<Effet>();
        #endregion
    }
}