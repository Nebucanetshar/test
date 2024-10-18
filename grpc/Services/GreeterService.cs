using Grpc.Core;
namespace grpc.Services;

public class GreeterService : Merge.MergeBase
{
	private readonly ILogger<GreeterService>_logger;
    private readonly AppDbContext _appDbContext;
	public GreeterService(ILogger<GreeterService> logger ,AppDbContext dbContext)
	{
		_logger = logger;
        _appDbContext = dbContext;
        
	}

	public override async Task SayHelloStream(CounterRequest request, IServerStreamWriter<CounterResponse> responseStream, ServerCallContext context)
    {
        if (request.Count <=0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "le compteur doit $etre plus grand que Zeros"));
        }
        _logger.LogInformation($"l'utilisateur à appuyer {request.Count} fois");

        for (var i=0; i<request.Count; i++)
        {
            await responseStream.WriteAsync(new CounterResponse { Message = $"tu a réussi a joindre les deux bout {i+1} fois " });
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
    //public override async Task<CreateTodoRequest> CreateTodo(CreateTodoRequest request, ServerCallContext context)
    //{
    //    var item = new Counter
    //    {
    //        Title = request.Title,
    //        Description = request.Description,
    //    };

    //    if (request.Title == string.Empty || request.Description == string.Empty)
    //    {
    //        throw new RpcException(new Status(StatusCode.InvalidArgument, "la requête ne peut etre vide"));
    //    }

    //    await _appDbContext.AddAsync(item);
    //    await _appDbContext.SaveChangesAsync();

    //    return await Task.FromResult(new CounterResponse
    //    {
    //        Id = item.Id,
    //    });
    //}

}
