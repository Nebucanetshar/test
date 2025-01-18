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
        var click = request.Start;

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
    public GrpcService _grpcClient = new GrpcService();

    public override async Task StartCounter(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
    {
        await _grpcClient.Stream(request, response, context);

    }
}