using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace grpc.Services;

public interface IGrpcClient
{
    Task Stream(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context);
}

public class GrpcService : IGrpcClient
{
    public GrpcService() { }

    public async Task Stream(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
    {
        var count = request.Start;

        while (!context.CancellationToken.IsCancellationRequested)
        {
            ++count;

            await response.WriteAsync(new CounterResponse
            {
                Count = count
            });

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}

public class CounterServer: Counter.CounterBase
{
    public readonly AppDbContext _appDbContext;
    public GrpcService _grpcClient = new GrpcService();

    public CounterServer(AppDbContext context)
    {
        _appDbContext = context;
    }

    public override async Task StartCounter(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
    {
        await _grpcClient.Stream(request, response, context);

        try
        {
            await _appDbContext.Items.ToListAsync();
            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Trace.TraceInformation($"la collect n'a pas était effectuée {ex.Message}");
        }

    }
}