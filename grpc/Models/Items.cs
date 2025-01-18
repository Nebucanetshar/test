using grpc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grpc;

public class Items
{
    [Key] public int Id { get; set; }
    public int CurrentCount { get; set; }
    public DateTime Timestamp { get; set; }
}
