using Grpc.Core;

namespace grpc;

public class CounterServer:Counter.CounterBase
{
    public override async Task StartCounter(CounterRequest request, IServerStreamWriter<CounterResponse> response, ServerCallContext context)
    {
        var count = request.Start;

        while (!context.CancellationToken.IsCancellationRequested)
        {
            await response.WriteAsync(new CounterResponse
            {
                Count = ++count
            });

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
