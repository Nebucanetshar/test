using Grpc.Core;
using System.Diagnostics;
using grpc.Models;
using LinqToDB;
using LinqToDB.SqlQuery;
using Google.Protobuf.WellKnownTypes;


namespace grpc.Services;

public interface IGrpcClient
{
    Task Stream(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context);
}

public class GrpcService : IGrpcClient
{
    public DataOptions _dataOptions;
    public DataOptions<AppDataConnection> _options;
    public AppDataConnection _connection;
    public CounterState state = new CounterState();
    public GrpcService()
    {
        _dataOptions = new DataOptions();
        _options = new DataOptions<AppDataConnection>(_dataOptions);
        _connection = new AppDataConnection(_options);
    }
    
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

            try
            {
               var result = _connection.ToDb.Insert( () => new ToDb
                {
                    CurrentCount = state.GetCount(),
                    Timestamp = DateTime.Now
                });

            }
            catch (Exception ex)
            {
                Trace.TraceInformation($"Erreur dû a : {ex.Message}");
            }
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