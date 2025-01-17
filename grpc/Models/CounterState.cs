namespace grpc.Models;

public class CounterState
{
    
    private int _count;
    private readonly object _lock = new object();

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

    public void SetCount(int value)
    {
        lock (_lock)
        {
            _count = value;
        }
    }

}
