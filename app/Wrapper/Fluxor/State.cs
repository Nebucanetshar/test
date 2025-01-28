using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;


public record CountState
{
    public int CurrentCount { get; init; } = 0;
    public bool IsCounting { get; init; } = false;
}
