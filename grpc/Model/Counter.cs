using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace grpc;

public class Counter
{
    [Key] public int count { get; set; }
    public string? message { get; set; } 

}
