namespace grpc.Models;

public class CounterState
{
    
    private int _count;
    private readonly object _lock = new object(); // thread-safe ??

    public int GetCount()
    {
        lock (_lock)
        {
            return _count;
        }
    }

    public void Increment()
    {
        lock(_lock)
        {
            _count++;
        }

    }

}
