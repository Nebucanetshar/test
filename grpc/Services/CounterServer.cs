using Grpc.Core;

namespace grpc;

public class CounterServer:Counter.CounterBase
{
    private readonly AppDbContext _appDbContext;

    public CounterServer(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public override async Task StartCounter(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
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
