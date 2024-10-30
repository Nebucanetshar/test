using System.ComponentModel.DataAnnotations;

namespace grpc;

public class Items
{
    [Key] public int Id { get; set; }
    public int CurrentCount { get; set; }
    public DateTime Timestamp { get; set; }
}
