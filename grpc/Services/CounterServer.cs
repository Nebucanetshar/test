using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace grpc;

public interface IGrpcClient
{
    Task Stream(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context);
}

public class CounterServer : IGrpcClient
{
    private readonly AppDbContext _appDbContext;
    
    public async Task Stream(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
    {
        var count = request.Start;

        while (!context.CancellationToken.IsCancellationRequested)
        {
            ++count;

            var counter = new Items
            {
                CurrentCount = count,
                Timestamp = DateTime.UtcNow,
            };

            _appDbContext.Items.Add(counter);
            await _appDbContext.SaveChangesAsync();

            await response.WriteAsync(new CounterResponse
            {
                Count = count
            });

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}

public class GrpcService : Counter.CounterBase
{
    private readonly IGrpcClient _grpcClient;
    public GrpcService(IGrpcClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    public override async Task StartCounter(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
    {
        await _grpcClient.Stream(request, response, context);
    }

}

    

    

