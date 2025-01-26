using Grpc.Core;
using System.Diagnostics;
using grpc.Models;
using LinqToDB;
using LinqToDB.SqlQuery;


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
                var result = await _connection
                    .GetTable<ToDb>()
                    .Where(items => items.CurrentCount == state.GetCount())
                    .Where(items => items.Timestamp == DateTime.UtcNow)
                    .FirstOrDefaultAsync(context.CancellationToken);
                
                if (result != null)
                    await _connection.UpdateAsync(result);
                else
                    Trace.TraceInformation("Aucun élément trouvé à mettre à jour");
                
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