using grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using System;
using System.Runtime.CompilerServices;
using test;
using Xunit;

class Program : IClassFixture<WebApplicationFactory<AppProgram>>
{
    static async Task Main()
    {
        WebApplicationFactory<AppProgram> _factory = new WebApplicationFactory<AppProgram>();
        
        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(service =>
            {
                service.AddGrpcClient<Merge.MergeClient>(o =>
                {
                    o.Address = new Uri("https://localhost7091");
                });
            });
        });

        var serviceProvider = client.Services;

        // On vérifie si Merge.MergeClient est enregistré dans le service provider
        var isServiceAdded = serviceProvider.GetService<Merge.MergeClient>() != null;

        if (isServiceAdded)
        {
            // Si le service a bien été ajouté
            Console.WriteLine("Le service Merge.MergeClient a été ajouté au conteneur.");
        }
        else
        {
            // Si le service n'est pas trouvé, lancer une exception
            throw new InvalidOperationException("Le service Merge.MergeClient n'a pas été ajouté au conteneur.");
        }
    }
       
}

        
        
        
       



