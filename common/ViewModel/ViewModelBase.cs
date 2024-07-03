using System.Text.Json;

namespace common.ViewModel;

public class ViewModelBase
{
    public string ToStringJson()
    {
        return JsonSerializer.Serialize(this);
    }
}
