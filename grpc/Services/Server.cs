using grpc.Data;
using grpc.Model;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;



namespace grpc.Services;

public class Server : Do.DoBase
{
    private readonly AppDbContext _dbContext;

    public Server(AppDbContext dbContext)
    {
        _dbContext = dbContext;

    }
    #region Création
    public override async Task<CreateResponse> DoCreate(CreateRequest request, ServerCallContext context)
    {
        if (request.Title == string.Empty || request.Description == string.Empty) // verifie si les requêtes ne sont pas vide 
            throw new RpcException(new Status(StatusCode.InvalidArgument, "une des requêtes est vide"));
        // interruption du flux (appel à distance) ! si la condition est fausse l'exception RPC s'affiche 

        var items = new Items // initilisation de items avec les propriétés 
        {
            Title = request.Title,
            Description = request.Description,
        };
        await _dbContext.AddAsync(items); // ajout des éléments à la base de donnée 
        await _dbContext.SaveChangesAsync(); // enregistre les modifications de manière asynchrone 

        return await Task.FromResult(new CreateResponse // retourne l'instance contenant l'id de l'élément nouvellement crée 
        {
            Id = items.Id,
        });
    }
    #endregion

    #region Lecture
    public override async Task<ReadResponse> DoRead(ReadRequest request, ServerCallContext context)
    {
        if (request.Id <= 0) // vérifie si l'id de la requête est inférieur ou égale a 0 
            throw new RpcException(new Status(StatusCode.InvalidArgument, "l'Id doit être inférieur ou égale a 0"));
        // interruption du flux (appel à distance) ! si la condition est fausse l'exception RPC s'affiche 

        var items = await _dbContext.Items.FirstOrDefaultAsync(o => o.Id == request.Id);
        // o représente chaque élément de la table 
        // retourne le premier élément de la table qui satisfait la condition qui vérifie si l'ID de l'élément (o.id) correspond à l'ID spécifié dans request.Id.

        if (items != null)
        {
            return await Task.FromResult(new ReadResponse // retourne l'instance contenant l'id de l'élément nouvellement crée 
            {
                Id = items.Id,
                Title = items.Title,
                Description = items.Description,
            });
        }
        throw new RpcException(new Status(StatusCode.NotFound, $"la requête demander n'a pas était trouver{request.Id}"));
        // indiquant que la requête demandée n'a pas été trouvée. Par exemple, si request.Id a une valeur de 123, la chaîne résultante sera "la requete demander n'a pas était trouver 123".
    }
    #endregion

    #region Liste
    public override async Task<GetAllResponse> DoList(GetAllRequest request, ServerCallContext context)
    {
        var response = new GetAllResponse();
        var items = await _dbContext.Items.ToListAsync();
        // récupère toutes les entrées de la table todoItems de la base de données et les renvoie sous forme de liste.

        foreach (var item in items) // répond à la requête tout en parcourant les éléments de la table pour ajouter les éléments suivant 
        {
            response.Id.Add(new ReadResponse
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
            });
        }
        return await Task.FromResult(response); // création d'une tâche qui renvoie comme resultat le contenue de la valeurs response 
    }
    #endregion

    #region Mise à jour
    public override async Task<UpdateResponse> DoUpdate(UpdateRequest request, ServerCallContext context)
    {
        if (request.Id <= 0 || request.Title == string.Empty || request.Description == string.Empty) // verifie si les requêtes ne sont pas vide 
            throw new RpcException(new Status(StatusCode.InvalidArgument, "vous devez soumettre un object valid"));
        // interruption du flux (appel à distance) ! si la condition est fausse l'exception RPC s'affiche 

        var items = await _dbContext.Items.FirstOrDefaultAsync(o => o.Id == request.Id);
        // o représente chaque élément de la table 
        // retourne le premier élément de la table qui satisfait la condition qui vérifie si l'ID de l'élément (o.id) correspond à l'ID spécifié dans request.Id.
        if (items == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"aucune tâche connu avec l'id {request.Id}"));
        // indiquant que la requête demandée n'a pas été trouvée. Par exemple, si request.Id a une valeur de 321, la chaîne résultante sera "No Task with id 321".
        items.Title = request.Title;
        items.Description = request.Description;

        await _dbContext.SaveChangesAsync();// enregistre les modifications de manière asynchrone 

        return await Task.FromResult(new UpdateResponse // création d'une tâche qui renvoie comme resultat le contenue de l'instanciation UpdateTodoResponse 
        {
            Id = items.Id,
        });
    }
    #endregion

    #region Supprimer
    public override async Task<DeleteResponse> DoDelete(DeleteRequest request, ServerCallContext context)
    {
        if (request.Id <= 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "ne peut être supprimer si l'id est inférieur ou égale à 0"));
        // interruption du flux (appel à distance) ! si la condition est fausse l'exception RPC s'affiche 

        var items = await _dbContext.Items.FirstOrDefaultAsync(o => o.Id == request.Id);
        // o représente chaque élément de la table 
        // retourne le premier élément de la table qui satisfait la condition qui vérifie si l'ID de l'élément (o.id) correspond à l'ID spécifié dans request.Id.

        if (items != null)
            throw new RpcException(new Status(StatusCode.NotFound, $"la requête demander n'a pas était trouver{request.Id}"));
        // indiquant que la requête demandée n'a pas été trouvée. Par exemple, si request.Id a une valeur de 321, la chaîne résultante sera "la requête demander n'a pas était trouver 321".

        _dbContext.Remove(items);// efface les éléments à la base de donnée 
        await _dbContext.SaveChangesAsync();// enregistre les modifications de manière asynchrone 

        return await Task.FromResult(new DeleteResponse // création d'une tâche qui renvoie comme resultat le contenue de l'instanciation DeleteTodoResponse 
        {
            Id = request.Id,
        });
    }
    #endregion
}


