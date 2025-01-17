using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using grpc.Models;

namespace grpc.Services;

public interface IGrpcClient
{
    Task Stream(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context);
}

public class GrpcService : IGrpcClient
{
    public CounterState state = new CounterState();
    public GrpcService() { }

    public async Task Stream(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
    {
        int start = request.Start;
        state.SetCount(start);


        while (!context.CancellationToken.IsCancellationRequested)
        {
            state.Increment();

            await response.WriteAsync(new CounterResponse
            {
                Count = state.GetCount()
            });

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}

public class CounterServer : Counter.CounterBase
{
    public readonly AppDbContext _appDbContext;
    public GrpcService _grpcClient = new GrpcService();
    
    public CounterState state = new CounterState();

    public CounterServer(AppDbContext context)
    {
        _appDbContext = context;
    }

    public override async Task StartCounter(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
    {
        await _grpcClient.Stream(request, response, context);

        try
        {
            var items = new Items
            {
                CurrentCount = state,
                Timestamp = DateTime.UtcNow,
            };

            _appDbContext.Items.Add(items);
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Trace.TraceInformation($"Erreur : {ex.InnerException?.Message}");
            }
        }
        catch (Exception ex)
        {
            Trace.TraceInformation($"la collect n'a pas était effectuée {ex.Message}");
        }

    }
}