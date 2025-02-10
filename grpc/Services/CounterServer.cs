using Grpc.Core;
using System.Diagnostics;
using grpc.Models;
using LinqToDB;

namespace grpc.Services;

public interface IGrpcClient
{
    Task Stream(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context);
}

public class GrpcService : IGrpcClient
{
    public CounterState _state;
    public DataOptions _dataOptions;
    public DataOptions<AppDataConnection> _options;
    public AppDataConnection _connection;
   
    public GrpcService()
    {
        _state = new CounterState();
        _dataOptions = new DataOptions();
        _options = new DataOptions<AppDataConnection>(_dataOptions);
        _connection = new AppDataConnection(_options);
    }
    
   public async Task Stream(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
   {
        var click = request.Start;

        while (!context.CancellationToken.IsCancellationRequested)
        {
            _state.Increment();

            await response.WriteAsync(new CounterResponse
            {
                Count = _state.GetCount()
            });

            await Task.Delay(TimeSpan.FromSeconds(1));

            try
            {
                var result = _connection.ToDb.Insert(() => new ToDb
                {
                    CurrentCount = _state.GetCount(),
                    Timestamp = DateTime.Now
                });

            }
            catch (Exception ex)
            {
                Trace.TraceInformation($"erreur LinQ dû à : {ex.Message}");
            }

            if (context.CancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("server mis en arrêt");
                break;
            }
        }
   }
}

public class CounterServer : Counter.CounterBase
{
    public GrpcService _gRpcClient;

    public CounterServer()
    {
        _gRpcClient = new GrpcService();
    }
    public override async Task StartCounter(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
    {
        await _gRpcClient.Stream(request, response, context);
    }
}